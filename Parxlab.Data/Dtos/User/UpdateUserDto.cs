using System;

namespace Parxlab.Data.Dtos.User
{
    public record UpdateUserDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; init; }
        public string Image { get; set; }
    }
}
