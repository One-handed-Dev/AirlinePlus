using Common.Presentation;
using System.Collections.Generic;
using ShopSection.Domain.FlightAgg;

namespace WebQuery.Contracts.Shop.Airline
{
    public class QueryAirline
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
