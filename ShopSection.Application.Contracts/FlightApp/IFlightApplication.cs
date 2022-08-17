using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.FlightApp
{
    public interface IFlightApplication : IBaseEfApplication<SaveFlight, SearchFlight, ViewFlight>
    {
        ViewSimple[] GetPersianDayOfWeekArray();
    }
}
