
namespace Parxlab.Data.Dtos.User
{
    public record UserLoginDto
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}