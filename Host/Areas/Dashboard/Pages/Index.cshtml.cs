using Microsoft.AspNetCore.Mvc.RazorPages;
using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace Host.Areas.Dashboard.Pages
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IDashboardStatisticApplication dashboardStatisticApplication;

        public DashboardStatistic Statistic { get; set; }

        public IndexModel(IDashboardStatisticApplication dashboardStatisticApplication) => this.dashboardStatisticApplication = dashboardStatisticApplication;
        #endregion

        public void OnGet() => Statistic = dashboardStatisticApplication.Get();
    }
}
