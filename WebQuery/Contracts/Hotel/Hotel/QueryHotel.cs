using Common.Presentation;
using System.Collections.Generic;
using ShopSection.Domain.RoomAgg;
using WebQuery.Contracts.Shop.ShopPicture;

namespace WebQuery.Contracts.Shop.Shop
{
    public class QueryShop
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
        public List<QueryShopPicture> ShopPictures { get; set; }
    }
}
