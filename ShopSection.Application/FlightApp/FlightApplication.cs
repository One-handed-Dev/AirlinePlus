using Common.Application;
using ShopSection.Domain.FlightAgg;
using Common.Application.Contracts;
using ShopSection.Application.Contracts.Common;
using ShopSection.Application.Contracts.FlightApp;

namespace ShopSection.Application.FlightApp
{
    public class FlightApplication : BaseEfApplication<IFlightRepo, SaveFlight, SearchFlight, ViewFlight, Flight>, IFlightApplication
    {
        #region Init
        public FlightApplication(IFlightRepo repo) : base(repo) { }
        #endregion

        public override TaskResult Edit(SaveFlight command) => Edit(command, x => 
            x.DestinationCity == command.DestinationCity &&
            x.SourceCity == command.SourceCity &&
            x.StartHour == command.StartHour &&
            x.Day == command.Day
        );

        public override TaskResult Create(SaveFlight command) => Create(command, x => 
            x.DestinationCity == command.DestinationCity &&
            x.SourceCity == command.SourceCity &&
            x.StartHour == command.StartHour &&
            x.Day == command.Day
        );

        public ViewSimple[] GetPersianDayOfWeekArray() => ShopUtility.GetPersianDayOfWeekArray();
    }
}
