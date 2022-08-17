namespace ShopSection.Application.Contracts.OrderApp
{
    public sealed class ViewOrderItem
    {
        public long FlightId { get; set; }
        public long OrderId { get; set; }
        public double Price { get; set; }
        public string FlightName { get; set; }
        public string EndDatePr { get; set; }
        public int CountOfFlights { get; set; }
        public int CountOfNights { get; set; }
        public string StartDatePr { get; set; }
        public double TotalPayAmount { get; set; }
    }
}
