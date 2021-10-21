namespace Parxlab.Data.Dtos
{
   public record RegisterUserDto
    {
        public string Username { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string Role{ get; init; }
    }
}
