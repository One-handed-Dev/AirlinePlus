using System.Collections.Generic;
using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.RoleApp
{
    public interface IRoleApplication : IBaseEfApplication<SaveRole, SearchRole, ViewRole>
    {
        bool HasPermission(HasPermissionDto command);
        List<int> GetPermissionsByRoleId(long roleId);
    }
}
