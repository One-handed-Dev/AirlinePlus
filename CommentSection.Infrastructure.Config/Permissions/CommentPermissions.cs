using static Common.Application.Strings.Persian;

namespace CommentSection.Infrastructure.Config.Permissions
{
    public sealed class CommentPermissions
    {
        public static readonly string ForComment = PermissionDisplayNames.Comment;
        public enum Comment
        {
            List = 2000,
            Search = 2001,
            Cancel = 2002,
            Confirm = 2003,
            Send = 2004,
        }
    }
}
