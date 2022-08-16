using Common.Domain;
using Common.Infrastructure;
using System.Collections.Generic;
using static Common.Application.Strings.Persian;

namespace InteractionSection.Infrastructure.Config.Permissions
{
    class InteractionPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose() => new()
        {
            {
                InteractionPermissions.ForEmail,
                new List<PermissionDto>
                {
                    new((int)InteractionPermissions.Email.List,    PermissionDisplayNames.List),
                    new((int)InteractionPermissions.Email.Edit,    PermissionDisplayNames.Edit),
                    new((int)InteractionPermissions.Email.Send,    PermissionDisplayNames.Send),
                    new((int)InteractionPermissions.Email.Create,  PermissionDisplayNames.Create),
                    new((int)InteractionPermissions.Email.Search,  PermissionDisplayNames.Search),
                    new((int)InteractionPermissions.Email.Remove,  PermissionDisplayNames.Remove),
                    new((int)InteractionPermissions.Email.Restore, PermissionDisplayNames.Restore)
                }
            }
        };
    }
}
