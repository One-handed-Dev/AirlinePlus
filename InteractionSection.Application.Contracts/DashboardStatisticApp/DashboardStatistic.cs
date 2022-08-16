using System.Collections.Generic;
using ShopSection.Application.Contracts.ShopApp;
using ShopSection.Application.Contracts.OrderApp;

namespace InteractionSection.Application.Contracts.DashboardStatisticApp
{
    public class DashboardStatistic
    {
        public int UnreadCommentsCount { get; set; }
        public int LastMonthEnrollmentsCount { get; set; }
        public int LastMonthReservationsCount { get; set; }
        public List<ViewShop> FiveRecentlyShopDefinition { get; set; }
        public List<ViewOrder> FiveRecentlyShopReservations { get; set; }
    }
}
