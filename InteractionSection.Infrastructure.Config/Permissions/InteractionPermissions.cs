using static Common.Application.Strings.Persian;

namespace InteractionSection.Infrastructure.Config.Permissions
{
    public class InteractionPermissions
    {
        public static readonly string ForEmail = PermissionDisplayNames.Email;
        public enum Email
        {
            List = 4000,
            Search = 4001,
            Create = 4002,
            Edit = 4003,
            Remove = 4004,
            Restore = 4005,
            Send = 4006,
        }
    }
}
