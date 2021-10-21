
namespace Parxlab.Common.Api
{
    public record AuthResult : ApiResult
    {
        public string UserId { get; init; }
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}