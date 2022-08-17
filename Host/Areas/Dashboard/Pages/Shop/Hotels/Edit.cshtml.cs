using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Hotels
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication hotelApplication;

        public SaveHotel Command { get; set; }

        public EditModel(IAirlineApplication hotelApplication) => this.hotelApplication = hotelApplication;
        #endregion

        #region Edit
        [NeedsPermission(((int)ShopPermissions.Hotel.Edit))]
        public void OnGet(long id) => Command = hotelApplication.GetDetails(id);

        [NeedsPermission(((int)ShopPermissions.Hotel.Edit))]
        public IActionResult OnPost(SaveHotel command)
        {
            hotelApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
