using System;
using Common.Domain;
using Common.Application;
using ShopSection.Domain.AirlineAgg;

namespace ShopSection.Domain.FlightAgg
{
    public class Flight : BaseEfDomainModel
    {
        public double Price { get; set; }
        public int Capacity { get; set; }
        public int StartHour { get; set; }
        public DayOfWeek Day { get; set; }
        public long AirlineId { get; set; }
        public Airline Airline { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public int TimeOfFlightDurationHours { get; set; }

        public override void Edit<T>(T edited) => this.From(edited);
    }
}
