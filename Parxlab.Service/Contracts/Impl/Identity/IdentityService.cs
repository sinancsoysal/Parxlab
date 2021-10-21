using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Parxlab.Common.Api;
using Parxlab.Data;
using Parxlab.Data.Dtos;
using Parxlab.Entities;
using Parxlab.Entities.Identity;
using Parxlab.Repository;
using Parxlab.Service.Contracts.Identity;
using RepoDb;

namespace Parxlab.Service.Contracts.Impl.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper mapper;
        protected readonly IDbConnection dbConnection;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;

        public IdentityService(IMapper mapper, IDbConnection dbConnection,
            UserManager<User> userManager, RoleManager<Role> roleManager, ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.dbConnection = dbConnection;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Login(UserLoginDto userLogin)
        {
            var users = await dbConnection.ExecuteQueryAsync<User>(
                "SELECT TOP 1 * FROM [Users] WHERE Username = @username", new {username = userLogin.Username});
            if (!users.Any())
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.UnAuthorized,
                    Errors = new[] {"Giriş verileri doğru değil."}
                };
            }

            var user = users.FirstOrDefault();
            var userHasValidPassword = await userManager.CheckPasswordAsync(user, userLogin.Password);
            if (!userHasValidPassword)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.UnAuthorized,
                    Errors = new[] {"Giriş verileri doğru değil."}
                };
            }

            if (user.IsSuspended)
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.Forbidden,
                    Errors = new[] {"hesabınız yönetici tarafından askıya alındı"}
                };

            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, Constants.AdminRole),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, user.Id.ToString())
            };
            var roleClaims = await GetUserRoleClaims(user.Id);
            claims.AddRange(roleClaims.Select(roleClaim => new Claim(roleClaim.ClaimType, roleClaim.ClaimValue)));
            var tokenResult = tokenService.GenerateAccessToken(user, claims);
            var refreshToken = new RefreshToken()
            {
                JwtId = tokenResult.JwtId,
                UserId = user.Id,
                ExpirationDate = DateTime.UtcNow.AddMonths(6),
                Token = tokenService.GenerateRefreshToken()
            };
            await unitOfWork.RefreshToken.AddFast(refreshToken);
            return new AuthResult()
            {
                IsSuccess = true,
                Token = tokenResult.Token,
                RefreshToken = refreshToken.Token,
                UserId = refreshToken.UserId.ToString()
            };
        }

        public async Task<ApiResult> Register(RegisterUserDto registerUser)
        {
            var user = mapper.Map<RegisterUserDto, User>(registerUser);
            user.Id = Guid.NewGuid();
            var createdUser = await userManager.CreateAsync(user, registerUser.Password);
            if (!createdUser.Succeeded)
            {
                return new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.LogicError,
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            var res = await userManager.AddToRoleAsync(user, registerUser.Role);
            if (!res.Succeeded)
                return new ApiResult()
                {
                    IsSuccess = false,
                    Errors = res.Errors.Select(s => s.Description)
                };
            return new ApiResult()
            {
                IsSuccess = true
            };
        }


        public async Task<ApiResult> UpdateProfile(Guid userId, string name, string image)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.UnAuthorized,
                    Errors = new[] {"User does not exist"}
                };
            }

            user.Image = image;
            await userManager.UpdateAsync(user);
            return new ApiResult() {IsSuccess = true};
        }

        public async Task<ApiResult<UserDto>> GetUser(Guid userId)
        {
            var users = await dbConnection.ExecuteQueryAsync<UserDto>(
                "SELECT TOP 1 [Username],[Email],[DisplayName],[Image] FROM [Users] WHERE Id = @id",
                new {id = userId});
            if (!users.Any())
            {
                return new ApiResult<UserDto>()
                {
                    StatusCode = ApiResultStatusCode.UnAuthorized,
                    Errors = new[] {"User does not exist"}
                };
            }

            return new ApiResult<UserDto>()
            {
                IsSuccess = true,
                Data = users.FirstOrDefault()
            };
        }

        public async Task<AuthResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            //pass refresh token
            var principle = tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);
            if (principle == null)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Geçersiz Jeton"}
                };
            }

            var storedRefreshTokens = await unitOfWork.RefreshToken.GetByToken(refreshTokenRequest.RefreshToken);
            if (!storedRefreshTokens.Any())
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.NotFound,
                    Errors = new[] {"Yenileme Simgesi mevcut değil"}
                };
            }

            var storedRefreshToken = storedRefreshTokens.FirstOrDefault();
            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.Gone,
                    Errors = new[] {"Yenileme Simgesinin süresi doldu"}
                };
            }

            if (storedRefreshToken.IsInvalidated)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Yenileme Simgesi Geçersiz"}
                };
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Bu yenileme jetonu kullanıldı"}
                };
            }

            var jti = principle.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Bu yenileme belirteci bu JWT ile eşleşmiyor"}
                };
            }

            await unitOfWork.RefreshToken.SetUsed(storedRefreshToken.Id);
            var user = await userManager.FindByIdAsync(principle.FindFirstValue(ClaimTypes.Name));
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, Constants.AdminRole),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, user.Id.ToString())
            };
            var tokenResult = tokenService.GenerateAccessToken(user, claims);
            var refreshToken = new RefreshToken()
            {
                JwtId = tokenResult.JwtId,
                UserId = user.Id,
                ExpirationDate = DateTime.UtcNow.AddMonths(6),
                Token = tokenService.GenerateRefreshToken()
            };
            await unitOfWork.RefreshToken.AddFast(refreshToken);
            return new AuthResult()
            {
                IsSuccess = true,
                Token = tokenResult.Token,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<ApiResult> CreateRole(string name)
        {
            var res = await roleManager.CreateAsync(new Role(name));
            if (res.Succeeded)
                return new ApiResult()
                {
                    IsSuccess = true
                };
            return new ApiResult()
            {
                IsSuccess = false,
                Errors = res.Errors.Select(e => e.Description),
                StatusCode = ApiResultStatusCode.ServerError
            };
        }

        public async Task<ApiResult> DeleteRole(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
                return new ApiResult()
                {
                    IsSuccess = false,
                    Errors = new[] {"no such role name"}
                };
            await roleManager.DeleteAsync(role);
            return new ApiResult()
            {
                IsSuccess = true
            };
        }

        public Task<Role> GetRole(Guid id)
        {
            return roleManager.Roles.Include(i => i.Claims).FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Role>> GetAllRoles()
        {
            return roleManager.Roles.Include(r => r.Claims).ToListAsync();
        }

        public Task<List<UserDto>> GetAllMembers()
        {
            return userManager.Users.Include(i => i.Roles).Select(s => new UserDto()
            {
                Role = s.Roles.FirstOrDefault().Role.Name,
                DisplayName = s.FirstName + " " + s.LastName,
                Email = s.Email,
                Id = s.Id,
                Image = s.Image,
                Username = s.UserName
            }).ToListAsync();
        }

        public Task<IEnumerable<RoleClaim>> GetUserRoleClaims(Guid userId)
        {
            return dbConnection.ExecuteQueryAsync<RoleClaim>(
                @"SELECT [r0].[Id], [r0].[ClaimType], [r0].[ClaimValue], [r0].[RoleId]
                         FROM [dbo].[UserRole] AS [u]
                         INNER JOIN [dbo].[Roles] AS [r] ON [u].[RoleId] = [r].[Id]
                         INNER JOIN [dbo].[RoleClaim] AS [r0] ON [r].[Id] = [r0].[RoleId]
                         WHERE [u].[UserId] = @userId ", new {userId});
        }

        public async Task<ApiResult> SuspendUser(Guid id)
        {
            var user = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (user == null)
                return new ApiResult()
                {
                    IsSuccess = false,
                    Errors = new[] {"No such user"}
                };
            user.IsSuspended = true;
            var res = await userManager.UpdateAsync(user);
            if (res.Succeeded)
                return new ApiResult()
                {
                    IsSuccess = true
                };
            return new ApiResult()
            {
                IsSuccess = false,
                Errors = res.Errors.Select(s => s.Description)
            };
        }
    }
}