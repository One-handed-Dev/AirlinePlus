using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSection.Application.Contracts.AirlineApp;
using ShopSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Airlines
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication airlineApplication;

        public SearchAirline Command { get; set; }
        public List<ViewAirline> List { get; set; }
        [TempData] public string Message { get; set; }

        public IndexModel(IAirlineApplication airlineApplication) => this.airlineApplication = airlineApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)ShopPermissions.Airline.List))]
        public void OnGet(SearchAirline command)
        {
            List = airlineApplication.Search(command);
        }
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)ShopPermissions.Airline.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = airlineApplication.Remove(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)ShopPermissions.Airline.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = airlineApplication.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
