using static Common.Application.Strings.Persian;

namespace ShopSection.Infrastructure.Config.Permissions
{
    public class ShopPermissions
    {
        public static readonly string ForFlight = PermissionDisplayNames.Flight;
        public enum Flight
        {
            List    = 3000,
            Search  = 3001,
            Create  = 3002,
            Edit    = 3003,
            Remove  = 3004,
            Restore = 3005,
        }

        public static readonly string ForAirline = PermissionDisplayNames.Airline;
        public enum Airline
        {
            List    = 3010,
            Search  = 3011,
            Create  = 3012,
            Edit    = 3013,
            Remove  = 3014,
            Restore = 3015,
        }

        public static readonly string ForOrder = PermissionDisplayNames.Order;
        public enum Order
        {
            List    = 3020,
            Search  = 3021,
            Confirm = 3022,
            Cancel  = 3023,
            Log     = 3024,
            Edit    = 3026,
        }
    }
}
