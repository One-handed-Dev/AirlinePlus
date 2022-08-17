using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Hotels
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication hotelApplication;

        public SearchHotel Command { get; set; }
        public List<ViewHotel> List { get; set; }
        [TempData] public string Message { get; set; }

        public IndexModel(IAirlineApplication hotelApplication) => this.hotelApplication = hotelApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)ShopPermissions.Hotel.List))]
        public void OnGet(SearchHotel command) => List = hotelApplication.Search(command);
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)ShopPermissions.Hotel.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = hotelApplication.Remove(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)ShopPermissions.Hotel.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = hotelApplication.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
