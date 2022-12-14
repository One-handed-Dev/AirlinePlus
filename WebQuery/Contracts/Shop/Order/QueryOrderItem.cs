namespace WebQuery.Contracts.Shop.Order
{
    public class QueryOrderItem
    {
        public string Name { get; set; }
        public long ShopId { get; set; }
        public long OrderId { get; set; }
        public double Price { get; set; }
        public long FlightId { get; set; }
        public string Picture { get; set; }
        public string EndDatePr { get; set; }
        public int CountOfNights { get; set; }
        public int CountOfFlights { get; set; }
        public string StartDatePr { get; set; }
        public double TotalItemPrice { get; set; }
    }
}
