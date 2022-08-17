using Common.Domain;
using ShopSection.Application.Contracts.AirlineApp;

namespace ShopSection.Domain.AirlineAgg
{
    public interface IAirlineRepo : IBaseEfRepo<SaveAirline, SearchAirline, ViewAirline, Airline>
    {
    }
}
