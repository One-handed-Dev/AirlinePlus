using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace InteractionSection.Domain.DashboardStatisticAgg
{
    public interface IDashboardStatisticRepo
    {
        DashboardStatistic Get();
    }
}
