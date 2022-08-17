using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;
using ShopSection.Application.Contracts.AirlineApp;

namespace Host.Areas.Dashboard.Pages.Shop.Hotels
{
    public sealed class CreateModel : PageModel
    {
        #region Init
        private readonly IAirlineApplication hotelApplication;

        public SaveHotel Command { get; set; }

        public CreateModel(IAirlineApplication hotelApplication) => this.hotelApplication = hotelApplication;
        #endregion

        #region Create
        [NeedsPermission(((int)ShopPermissions.Hotel.Create))]
        public void OnGet() { }

        [NeedsPermission(((int)ShopPermissions.Hotel.Create))]
        public IActionResult OnPost(SaveHotel command)
        {
            hotelApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
