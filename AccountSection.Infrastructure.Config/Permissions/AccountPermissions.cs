using static Common.Application.Strings.Persian;

namespace AccountSection.Infrastructure.Config.Permissions
{
    public static class AccountPermissions
    {
        public static readonly string ForRole = PermissionDisplayNames.Role;
        public enum Role
        {
            List = 1000,
            Search = 1001,
            Create = 1002,
            Edit = 1003,
            Remove = 1004,
            Restore = 1005,
        }

        public static readonly string ForAccount = PermissionDisplayNames.Account;
        public enum Account
        {
            List = 1010,
            Search = 1011,
            Create = 1012,
            Edit = 1013,
            Remove = 1014,
            Restore = 1015,
            ChangePassword = 1016,
            Ban = 1017,
            ChangeRole = 1018,
            Unban = 1019,
        }

        public static readonly string ForForgetPasswordLog = PermissionDisplayNames.ForgetPasswordLog;
        public enum ForgetPasswordLog
        {
            List = 1020,
            Search = 1021,
            Create = 1022,
            Edit = 1023,
            Remove = 1024,
            Restore = 1025,
        }

        public static readonly string ForAccountPolicy = PermissionDisplayNames.AccountPolicy;
        public enum AccountPolicy
        {
            Edit = 1030,
        }
    }
}
