using Common.Presentation;
using ShopSection.Domain.FlightAgg;
using System.Collections.Generic;
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
        public int CountOfFlights { get; set; }
        public List<Flight> Flights { get; set; }
        public List<QueryComment> Comments { get; set; }
    }
}
