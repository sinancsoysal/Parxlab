using System;

namespace Parxlab.Data.Dtos.User
{
    public record UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string Image { get; set; }
    }
}
