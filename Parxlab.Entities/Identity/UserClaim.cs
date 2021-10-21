using System;
using Microsoft.AspNetCore.Identity;

namespace Parxlab.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public virtual User User { get; set; }
    }
}