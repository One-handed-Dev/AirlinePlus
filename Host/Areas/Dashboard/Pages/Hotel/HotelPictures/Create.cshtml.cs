using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;
using HotelSection.Application.Contracts.HotelPictureApp;

namespace Host.Areas.Dashboard.Pages.Hotel.HotelPictures
{
    public sealed class CreateModel : PageModel
    {
        #region Init
        private readonly IHotelApplication hotelApplication;
        private readonly IHotelPictureApplication hotelPictureApplication;

        public List<ViewHotel> Hotels { get; set; }
        public SaveHotelPicture Command { get; set; }

        public CreateModel(IHotelApplication hotelApplication, IHotelPictureApplication hotelPictureApplication)
        {
            this.hotelApplication = hotelApplication;
            this.hotelPictureApplication = hotelPictureApplication;
        }
        #endregion

        #region Create
        [NeedsPermission(((int)HotelPermissions.HotelPicture.Create))]
        public void OnGet(long id) => Hotels = hotelApplication.GetSelectList();

        [NeedsPermission(((int)HotelPermissions.HotelPicture.Create))]
        public IActionResult OnPost(SaveHotelPicture command)
        {
            hotelPictureApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
