using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.AirlineApp
{
    public interface IAirlineApplication : IBaseEfApplication<SaveAirline, SearchAirline, ViewAirline>
    {
    }
}
