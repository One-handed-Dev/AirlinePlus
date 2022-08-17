namespace HotelSection.Application.Contracts.OrderApp
{
    public sealed class ViewOrderItem
    {
        public long RoomId { get; set; }
        public long OrderId { get; set; }
        public double Price { get; set; }
        public string RoomName { get; set; }
        public string EndDatePr { get; set; }
        public int CountOfRooms { get; set; }
        public int CountOfNights { get; set; }
        public string StartDatePr { get; set; }
        public double TotalPayAmount { get; set; }
    }
}
