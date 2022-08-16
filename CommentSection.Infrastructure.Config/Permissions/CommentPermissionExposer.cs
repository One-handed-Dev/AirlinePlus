using Common.Domain;
using Common.Infrastructure;
using System.Collections.Generic;
using static Common.Application.Strings.Persian;

namespace CommentSection.Infrastructure.Config.Permissions
{
    public sealed class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    CommentPermissions.ForComment, new List<PermissionDto>
                    {
                        new((int)CommentPermissions.Comment.List,    PermissionDisplayNames.List.ToString()),
                        new((int)CommentPermissions.Comment.Send,    PermissionDisplayNames.Send.ToString()),
                        new((int)CommentPermissions.Comment.Search,  PermissionDisplayNames.Search.ToString()),
                        new((int)CommentPermissions.Comment.Cancel,  PermissionDisplayNames.Cancel.ToString()),
                        new((int)CommentPermissions.Comment.Confirm, PermissionDisplayNames.Confirm.ToString()),
                    }
                }
            };
        }
    }
}