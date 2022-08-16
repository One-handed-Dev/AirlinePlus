using InteractionSection.Domain.DashboardStatisticAgg;
using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace InteractionSection.Application.DashboardStatisticApp
{
    public class DashboardStatisticApplication : IDashboardStatisticApplication
    {
        #region Init
        private readonly IDashboardStatisticRepo repo;

        public DashboardStatisticApplication(IDashboardStatisticRepo repo) => this.repo = repo;
        #endregion

        public DashboardStatistic Get() => repo.Get();
    }
}
