using Common.Application.Contracts;
using System.Collections.Generic;

namespace AccountSection.Application.Contracts.RoleApp
{
    public interface IRoleApplication : IBaseEfApplication<SaveRole, SearchRole, ViewRole>
    {
        bool HasPermission(HasPermissionDto command);
        List<int> GetPermissionsByRoleId(long roleId);
    }
}
