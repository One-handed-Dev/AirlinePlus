using static Common.Application.Calendar;

namespace HotelSection.Application.Contracts.OrderApp
{
    public sealed class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long HotelId { get; set; }
        public double Price { get; set; }
        public string EndDate { get; set; }
        public string Picture { get; set; }
        public int CountOfRooms { get; set; }
        public int CountOfNights { get; set; }
        public string StartDate { get; set; }
        public double PayAmount { get; set; }
        public double TotalItemPrice { get; set; }

        public double CalculateTotalItemPrice()
        {
            var endDatePr = EndDate.FromJalaliString();
            var startDatePr = StartDate.FromJalaliString();
            CountOfNights = (endDatePr - startDatePr).Value.Days;
            return TotalItemPrice = Price * CountOfRooms * CountOfNights;
        }
    }
}
