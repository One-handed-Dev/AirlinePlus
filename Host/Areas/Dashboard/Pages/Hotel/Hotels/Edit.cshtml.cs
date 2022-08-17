using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Hotel.Hotels
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IHotelApplication hotelApplication;

        public SaveHotel Command { get; set; }

        public EditModel(IHotelApplication hotelApplication) => this.hotelApplication = hotelApplication;
        #endregion

        #region Edit
        [NeedsPermission(((int)HotelPermissions.Hotel.Edit))]
        public void OnGet(long id) => Command = hotelApplication.GetDetails(id);

        [NeedsPermission(((int)HotelPermissions.Hotel.Edit))]
        public IActionResult OnPost(SaveHotel command)
        {
            hotelApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
