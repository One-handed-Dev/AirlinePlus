using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Hotel.Hotels
{
    public sealed class CreateModel : PageModel
    {
        #region Init
        private readonly IHotelApplication hotelApplication;

        public SaveHotel Command { get; set; }

        public CreateModel(IHotelApplication hotelApplication) => this.hotelApplication = hotelApplication;
        #endregion

        #region Create
        [NeedsPermission(((int)HotelPermissions.Hotel.Create))]
        public void OnGet() { }

        [NeedsPermission(((int)HotelPermissions.Hotel.Create))]
        public IActionResult OnPost(SaveHotel command)
        {
            hotelApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
