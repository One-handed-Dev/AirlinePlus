namespace WebQuery.Contracts.Hotel.Order
{
    public class QueryOrderItem
    {
        public string Name { get; set; }
        public long RoomId { get; set; }
        public long HotelId { get; set; }
        public long OrderId { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public string EndDatePr { get; set; }
        public int CountOfRooms { get; set; }
        public int CountOfNights { get; set; }
        public string StartDatePr { get; set; }
        public double TotalItemPrice { get; set; }
    }
}
