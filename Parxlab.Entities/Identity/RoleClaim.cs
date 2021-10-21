using System;
using Microsoft.AspNetCore.Identity;

namespace Parxlab.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; set; }
    }
}