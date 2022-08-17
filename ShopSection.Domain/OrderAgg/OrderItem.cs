using Common.Domain;

namespace ShopSection.Domain.OrderAgg
{
    public sealed class OrderItem : BaseEfDomainModel
    {
        public long FlightId { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        public double Price { get; set; }
        public string EndDatePr { get; set; }
        public int CountOfFlights { get; set; }
        public int CountOfNights { get; set; }
        public string StartDatePr { get; set; }

        internal OrderItem() { }

        public OrderItem(long roomId, double price, int countOfFlights, int countOfNights, string startDatePr, string endDatePr)
        {
            Price = price;
            FlightId = roomId;
            EndDatePr = endDatePr;
            StartDatePr = startDatePr;
            CountOfFlights = countOfFlights;
            CountOfNights = countOfNights;
        }
    }
}
