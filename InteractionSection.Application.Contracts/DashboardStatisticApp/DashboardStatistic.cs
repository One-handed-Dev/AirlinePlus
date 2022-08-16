using System.Collections.Generic;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Application.Contracts.OrderApp;

namespace InteractionSection.Application.Contracts.DashboardStatisticApp
{
    public class DashboardStatistic
    {
        public int UnreadCommentsCount { get; set; }
        public int LastMonthEnrollmentsCount { get; set; }
        public int LastMonthReservationsCount { get; set; }
        public List<ViewHotel> FiveRecentlyHotelDefinition { get; set; }
        public List<ViewOrder> FiveRecentlyHotelReservations { get; set; }
    }
}
