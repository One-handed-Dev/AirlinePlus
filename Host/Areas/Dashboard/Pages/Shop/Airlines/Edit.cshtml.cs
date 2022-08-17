using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSection.Application.Contracts.AirlineApp;
using ShopSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Airlines
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication airlineApplication;

        public SaveAirline Command { get; set; }

        public EditModel(IAirlineApplication airlineApplication) => this.airlineApplication = airlineApplication;
        #endregion

        #region Edit
        [NeedsPermission(((int)ShopPermissions.Airline.Edit))]
        public void OnGet(long id) => Command = airlineApplication.GetDetails(id);

        [NeedsPermission(((int)ShopPermissions.Airline.Edit))]
        public IActionResult OnPost(SaveAirline command)
        {
            airlineApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
