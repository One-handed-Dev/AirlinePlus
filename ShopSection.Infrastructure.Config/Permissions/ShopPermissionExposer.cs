using Common.Domain;
using Common.Infrastructure;
using System.Collections.Generic;
using static Common.Application.Strings.Persian;

namespace ShopSection.Infrastructure.Config.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose() => new()
        {
            {
                ShopPermissions.ForFlight, new List<PermissionDto>
                {
                    new((int)ShopPermissions.Flight.List,    PermissionDisplayNames.List),
                    new((int)ShopPermissions.Flight.Edit,    PermissionDisplayNames.Edit),
                    new((int)ShopPermissions.Flight.Create,  PermissionDisplayNames.Create),
                    new((int)ShopPermissions.Flight.Search,  PermissionDisplayNames.Search),
                    new((int)ShopPermissions.Flight.Remove,  PermissionDisplayNames.Remove),
                    new((int)ShopPermissions.Flight.Restore, PermissionDisplayNames.Restore)
                }
            }, 

            {
                ShopPermissions.ForShop, new List<PermissionDto>
                {
                    new((int)ShopPermissions.Shop.List,    PermissionDisplayNames.List),
                    new((int)ShopPermissions.Shop.Edit,    PermissionDisplayNames.Edit),
                    new((int)ShopPermissions.Shop.Create,  PermissionDisplayNames.Create),
                    new((int)ShopPermissions.Shop.Search,  PermissionDisplayNames.Search),
                    new((int)ShopPermissions.Shop.Remove,  PermissionDisplayNames.Remove),
                    new((int)ShopPermissions.Shop.Restore, PermissionDisplayNames.Restore)
                }
            }, 

            {
                ShopPermissions.ForShopPicture, new List<PermissionDto>
                {
                    new((int)ShopPermissions.ShopPicture.List,    PermissionDisplayNames.List),
                    new((int)ShopPermissions.ShopPicture.Edit,    PermissionDisplayNames.Edit),
                    new((int)ShopPermissions.ShopPicture.Create,  PermissionDisplayNames.Create),
                    new((int)ShopPermissions.ShopPicture.Search,  PermissionDisplayNames.Search),
                    new((int)ShopPermissions.ShopPicture.Remove,  PermissionDisplayNames.Remove),
                    new((int)ShopPermissions.ShopPicture.Restore, PermissionDisplayNames.Restore)
                }
            }, 

            {
                ShopPermissions.ForOrder, new List<PermissionDto>
                {
                    new((int)ShopPermissions.Order.Log,     PermissionDisplayNames.Log),
                    new((int)ShopPermissions.Order.List,    PermissionDisplayNames.List),
                    new((int)ShopPermissions.Order.Edit,    PermissionDisplayNames.Edit),
                    new((int)ShopPermissions.Order.Cancel,  PermissionDisplayNames.Cancel),
                    new((int)ShopPermissions.Order.Search,  PermissionDisplayNames.Search),
                    new((int)ShopPermissions.Order.Confirm, PermissionDisplayNames.Confirm),
                }
            }
        };
    }
}
