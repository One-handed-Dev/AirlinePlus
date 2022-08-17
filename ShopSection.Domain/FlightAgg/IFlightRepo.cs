using Common.Domain;
using ShopSection.Application.Contracts.FlightApp;

namespace ShopSection.Domain.FlightAgg
{
    public interface IFlightRepo : IBaseEfRepo<SaveFlight, SearchFlight, ViewFlight, Flight>
    {
    }
}
