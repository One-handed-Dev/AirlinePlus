using Common.Domain;
using Common.Application;
using System.Collections.Generic;
using ShopSection.Domain.FlightAgg;

namespace ShopSection.Domain.AirlineAgg
{
    public class Airline : BaseEfDomainModel
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public string Picture { get; set; }
        public List<Flight> Flights { get; set; }

        public override void Edit<T>(T edited) => this.From(edited, Projection.CalendarMode.ToGregorian, Projection.NullOrEmptyPicture.Ignore);
    }
}
