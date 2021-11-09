using System;

namespace Parxlab.Data.Dtos.User
{
   public record UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Role{ get; set; }
    }
}
