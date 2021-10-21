using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Parxlab.Entities.Identity
{
    public sealed class Role : IdentityRole<Guid>
    {
        public Role(string name) : base(name)
        {
            Name = name;
        }

        public ICollection<UserRole> Users { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
    }
}