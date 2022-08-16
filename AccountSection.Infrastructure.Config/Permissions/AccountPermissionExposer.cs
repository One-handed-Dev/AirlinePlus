using Common.Domain;
using Common.Infrastructure;
using System.Collections.Generic;
using static Common.Application.Strings.Persian;

namespace AccountSection.Infrastructure.Config.Permissions
{
    public sealed class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    AccountPermissions.ForRole, new List<PermissionDto>
                    {
                        new((int)AccountPermissions.Role.List,    PermissionDisplayNames.List.ToString()),
                        new((int)AccountPermissions.Role.Edit,    PermissionDisplayNames.Edit.ToString()),
                        new((int)AccountPermissions.Role.Create,  PermissionDisplayNames.Create.ToString()),
                        new((int)AccountPermissions.Role.Search,  PermissionDisplayNames.Search.ToString()),
                        new((int)AccountPermissions.Role.Remove,  PermissionDisplayNames.Remove.ToString()),
                        new((int)AccountPermissions.Role.Restore, PermissionDisplayNames.Restore.ToString()),
                    }
                },

                {
                    AccountPermissions.ForAccount, new List<PermissionDto>
                    {
                        new((int)AccountPermissions.Account.Ban,            PermissionDisplayNames.Ban.ToString()),
                        new((int)AccountPermissions.Account.List,           PermissionDisplayNames.List.ToString()),
                        new((int)AccountPermissions.Account.Edit,           PermissionDisplayNames.Edit.ToString()),
                        new((int)AccountPermissions.Account.Unban,          PermissionDisplayNames.Unban.ToString()),
                        new((int)AccountPermissions.Account.Create,         PermissionDisplayNames.Create.ToString()),
                        new((int)AccountPermissions.Account.Search,         PermissionDisplayNames.Search.ToString()),
                        new((int)AccountPermissions.Account.Remove,         PermissionDisplayNames.Remove.ToString()),
                        new((int)AccountPermissions.Account.Restore,        PermissionDisplayNames.Restore.ToString()),
                        new((int)AccountPermissions.Account.ChangeRole,     PermissionDisplayNames.ChangeRole.ToString()),
                        new((int)AccountPermissions.Account.ChangePassword, PermissionDisplayNames.ChangePassword.ToString()),
                    }
                },

                {
                    AccountPermissions.ForForgetPasswordLog, new List<PermissionDto>
                    {
                        new((int)AccountPermissions.ForgetPasswordLog.List,    PermissionDisplayNames.List.ToString()),
                        new((int)AccountPermissions.ForgetPasswordLog.Edit,    PermissionDisplayNames.Edit.ToString()),
                        new((int)AccountPermissions.ForgetPasswordLog.Create,  PermissionDisplayNames.Create.ToString()),
                        new((int)AccountPermissions.ForgetPasswordLog.Search,  PermissionDisplayNames.Search.ToString()),
                        new((int)AccountPermissions.ForgetPasswordLog.Remove,  PermissionDisplayNames.Remove.ToString()),
                        new((int)AccountPermissions.ForgetPasswordLog.Restore, PermissionDisplayNames.Restore.ToString()),
                    }
                },

                {
                    AccountPermissions.ForAccountPolicy, new List<PermissionDto>
                    {
                        new((int)AccountPermissions.AccountPolicy.Edit,    PermissionDisplayNames.List.ToString()),
                    }
                }
            };
        }
    }
}
