using System.Collections.Generic;
using AccountSection.Application.Contracts.RoleApp;

namespace AccountSection.Application.Contracts.AccountApp
{
    public sealed record ChangeRoleDto(long AccountId, long NewRoleId, List<ViewRole> Roles, long ModifierAccountId);
}