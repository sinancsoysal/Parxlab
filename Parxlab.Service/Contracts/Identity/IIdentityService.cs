using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parxlab.Common.Api;
using Parxlab.Data.Dtos.User;
using Parxlab.Data.Dtos.Role;
using Parxlab.Entities.Identity;

namespace Parxlab.Service.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<AuthResult> Login(UserLoginDto userLogin);
        Task<ApiResult> Register(RegisterUserDto registerUser);
        Task<ApiResult> UpdateProfile(Guid userId,UpdateUserDto updateUserDto);
        Task<ApiResult> UpdateUserPassword(Guid userId, UpdateUserPasswordDto updateUserPasswordDto);
        Task<ApiResult<UserDto>> GetUser(Guid userId);
        Task<AuthResult> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        //Roles and Users management
        Task<ApiResult> CreateRole(string name);
        Task<ApiResult> DeleteRole(string roleName);
        Task<Role> GetRole(Guid id);
        Task<List<Role>> GetAllRoles();
        Task<ApiResult> SuspendUser(Guid id);
        Task<List<UserDto>> GetAllMembers();
        Task<IEnumerable<RoleClaim>> GetUserRoleClaims(Guid userId);
    }
}
