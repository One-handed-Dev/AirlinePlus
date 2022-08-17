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
                ShopPermissions.ForAirline, new List<PermissionDto>
                {
                    new((int)ShopPermissions.Airline.List,    PermissionDisplayNames.List),
                    new((int)ShopPermissions.Airline.Edit,    PermissionDisplayNames.Edit),
                    new((int)ShopPermissions.Airline.Create,  PermissionDisplayNames.Create),
                    new((int)ShopPermissions.Airline.Search,  PermissionDisplayNames.Search),
                    new((int)ShopPermissions.Airline.Remove,  PermissionDisplayNames.Remove),
                    new((int)ShopPermissions.Airline.Restore, PermissionDisplayNames.Restore)
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
