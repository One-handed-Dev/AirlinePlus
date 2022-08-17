using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSection.Application.Contracts.AirlineApp;
using ShopSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Airlines
{
    public sealed class CreateModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication airlineApplication;

        public SaveAirline Command { get; set; }

        public CreateModel(IAirlineApplication airlineApplication) => this.airlineApplication = airlineApplication;
        #endregion

        #region Create
        [NeedsPermission(((int)ShopPermissions.Airline.Create))]
        public void OnGet() { }

        [NeedsPermission(((int)ShopPermissions.Airline.Create))]
        public IActionResult OnPost(SaveAirline command)
        {
            airlineApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
