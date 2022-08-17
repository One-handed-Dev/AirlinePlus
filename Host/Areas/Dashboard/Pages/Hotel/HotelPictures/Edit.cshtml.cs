using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;
using HotelSection.Application.Contracts.HotelPictureApp;

namespace Host.Areas.Dashboard.Pages.Hotel.HotelPictures
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IHotelApplication hotelApplication;
        private readonly IHotelPictureApplication hotelPictureApplication;

        public List<ViewHotel> Hotels { get; set; }
        public SaveHotelPicture Command { get; set; }

        public EditModel(IHotelApplication hotelApplication, IHotelPictureApplication hotelPictureApplication)
        {
            this.hotelApplication = hotelApplication;
            this.hotelPictureApplication = hotelPictureApplication;
        }
        #endregion

        #region Edit
        [NeedsPermission(((int)HotelPermissions.HotelPicture.Edit))]
        public void OnGet(long id)
        {
            Hotels = hotelApplication.GetSelectList();
            Command = hotelPictureApplication.GetDetails(id);
        }

        [NeedsPermission(((int)HotelPermissions.HotelPicture.Edit))]
        public IActionResult OnPost(SaveHotelPicture command)
        {
            hotelPictureApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
