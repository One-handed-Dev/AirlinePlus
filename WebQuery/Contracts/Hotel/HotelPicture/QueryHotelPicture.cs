namespace WebQuery.Contracts.Hotel.HotelPicture
{
    public class QueryHotelPicture
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public bool IsRemoved { get; set; }
        public string Picture { get; set; }
    }
}
