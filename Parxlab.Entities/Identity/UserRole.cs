using System;
using Microsoft.AspNetCore.Identity;

namespace Parxlab.Entities.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}