using System.Collections.Generic;
using System.Security.Claims;
using Parxlab.Common.Api;
using Parxlab.Entities.Identity;

namespace Parxlab.Service.Contracts.Identity
{
   public interface ITokenService
    {
        GenerateTokenResult GenerateAccessToken(User user,List<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
