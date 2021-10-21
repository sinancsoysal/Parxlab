using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Parxlab.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuspended { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
        public virtual ICollection<ActivityLog> ActivityLog { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
        public virtual ICollection<ParkUsage> ParkUsage{ get; set; }
        public virtual ICollection<Reserved> Reserved{ get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
    }
}