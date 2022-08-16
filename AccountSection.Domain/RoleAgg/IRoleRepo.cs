using Common.Domain;
using System.Collections.Generic;
using AccountSection.Application.Contracts.RoleApp;

namespace AccountSection.Domain.RoleAgg
{
    public interface IRoleRepo : IBaseEfRepo<SaveRole, SearchRole, ViewRole, Role>
    {
        bool HasPermission(HasPermissionDto command);
        List<int> GetPermissionsByRoleId(long roleId);
    }
}
