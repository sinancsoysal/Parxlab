using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parxlab.Entities.Identity;

namespace Parxlab.Service.Contracts.Identity
{
    public interface IRoleService
    {
        Task<List<RoleClaim>> GetUserRoleClaims(Guid userId);
    }
}
