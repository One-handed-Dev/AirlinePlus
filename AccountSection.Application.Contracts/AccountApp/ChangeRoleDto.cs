using AccountSection.Application.Contracts.RoleApp;
using System.Collections.Generic;

namespace AccountSection.Application.Contracts.AccountApp
{
    public sealed record ChangeRoleDto(long AccountId, long NewRoleId, List<ViewRole> Roles, long ModifierAccountId);
}