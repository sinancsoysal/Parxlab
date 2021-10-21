using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Parxlab.Data.Dtos;
using Parxlab.Entities.Identity;

namespace Parxlab.Service.Contracts.Identity
{
    public interface IApplicationRoleManager
    {
        #region BaseClass
        IQueryable<Role> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<Role>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<Role> FindByIdAsync(Guid roleId);
        Task<Role> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(Role role);
        Task UpdateNormalizedRoleNameAsync(Role role);
        Task<string> GetRoleNameAsync(Role role);
        Task<IdentityResult> SetRoleNameAsync(Role role, string name);
        #endregion


        #region CustomMethod
        List<Role> GetAllRoles();
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> FindClaimsInRole(Guid RoleId);
        Task<IdentityResult> AddOrUpdateClaimsAsync(Guid RoleId, string RoleClaimType, IList<string> SelectedRoleClaimValues);
        #endregion

        #region Methods For Dto
        Task<List<RoleDto>> GetAllRolesDtoAsync();
        #endregion

    }
}
