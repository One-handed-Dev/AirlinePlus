using Common.Application;
using ShopSection.Application.Contracts.AirlineApp;
using ShopSection.Domain.AirlineAgg;

namespace ShopSection.Application.AirlineApp
{
    public class AirlineApplication : BaseEfApplication<IAirlineRepo, SaveAirline, SearchAirline, ViewAirline, Airline>, IAirlineApplication
    {
        #region Init
        public AirlineApplication(IAirlineRepo repo) : base(repo) { }
        #endregion
    }
}
