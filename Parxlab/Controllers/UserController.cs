using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parxlab.Common.Api;
using Parxlab.Common.Extensions;
using Parxlab.Controllers.Base;
using Parxlab.Data.Dtos.User;
using Parxlab.Entities;
using Parxlab.Entities.Enums;
using Parxlab.Repository;
using Parxlab.Service.Contracts.Identity;

namespace Parxlab.Controllers
{
    public class UserController : ApiBaseController
    {
        private readonly IIdentityService identityService;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            this.identityService = identityService;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var result = await identityService.RefreshToken(refreshTokenRequest);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto profile)
        {
            var userId = User.GetUserId();
            var res = await identityService.UpdateProfile(userId, profile);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            var userId = User.GetUserId();
            var res = await identityService.UpdateUserPassword(userId, updateUserPasswordDto);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return Ok(new AuthResult
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(s => s.ErrorMessage))
                });
            var res = await identityService.Login(loginDto);
            if (res.IsSuccess)
            {
                    await _unitOfWork.ActivityLog.AddFast(new ActivityLog
                    {
                        UserId = Guid.Parse(res.UserId),
                        Type = ActivityType.Login,
                        Identity = loginDto.Username
                    });
                    return Ok(new AuthResult
                    {
                        IsSuccess = true,
                        RefreshToken = res.RefreshToken,
                        Token = res.Token,
                    });
            }
            return Ok(new AuthResult
            {
                IsSuccess = false,
                Errors = res.Errors
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ApiResult
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(s => s.ErrorMessage)),
                    StatusCode = ApiResultStatusCode.BadRequest
                });
            }

            var result = await identityService.Register(userDto);
            return Ok(result);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResult<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.GetUserId();
            var res = await identityService.GetUser(userId);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetUserId();
            var user =await identityService.GetUser(userId);
            var tokens = await _unitOfWork.RefreshToken.GetUnusedToken(User.GetUserId());
            foreach (var token in tokens)
            {
                await _unitOfWork.RefreshToken.SetUsed(token.Id);
            }
            await _unitOfWork.ActivityLog.AddFast(new ActivityLog
            {
                UserId = userId,
                Type = ActivityType.Logout,
                Identity = user.IsSuccess ? user.Data.Username : ""
            });

            return Ok(new ApiResult {IsSuccess = true, StatusCode = ApiResultStatusCode.Success});
        }
    }
}