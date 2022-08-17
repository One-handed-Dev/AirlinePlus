using System;
using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.FlightApp
{
    public class SaveFlight : BaseEfSaveModel
    {
        public double Price { get; set; }
        public int Capacity { get; set; }
        public DayOfWeek Day { get; set; }
        public int StartHour { get; set; }
        public long AirlineId { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public int TimeOfFlightDurationHours { get; set; }
    }

    public class ViewFlight : BaseEfViewModel
    {
        public double Price { get; set; }
        public int Capacity { get; set; }
        public DayOfWeek Day { get; set; }
        public int StartHour { get; set; }
        public long AirlineId { get; set; }
        public string DayString { get; set; }
        public string SourceCity { get; set; }
        public string AirlineString { get; set; }
        public string DestinationCity { get; set; }
        public int TimeOfFlightDurationHours { get; set; }
    }

    public class SearchFlight : BaseEfSearchModel
    {
        public DayOfWeek Day { get; set; }
        public long AirlineId { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
    }
}
