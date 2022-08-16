using Common.Presentation;
using System.Collections.Generic;
using HotelSection.Domain.RoomAgg;
using WebQuery.Contracts.Hotel.HotelPicture;

namespace WebQuery.Contracts.Hotel.Hotel
{
    public class QueryHotel
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public int StarsCount { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public List<Room> Rooms { get; set; }
        public int CountOfRooms { get; set; }
        public string Facilities { get; set; }
        public List<QueryComment> Comments { get; set; }
        public List<QueryHotelPicture> HotelPictures { get; set; }
    }
}
