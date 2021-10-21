
namespace Parxlab.Common.Api
{
   public record GenerateTokenResult
    {
        public string JwtId { get; init; }
        public string Token { get; init; }
    }
}
