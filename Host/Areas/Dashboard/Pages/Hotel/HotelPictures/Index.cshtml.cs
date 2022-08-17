using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;
using HotelSection.Application.Contracts.HotelPictureApp;

namespace Host.Areas.Dashboard.Pages.Hotel.HotelPictures
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IHotelApplication hotelApplication;
        private readonly IHotelPictureApplication hotelPictureApplication;

        public List<ViewHotel> Hotels { get; set; }
        [TempData] public string Message { get; set; }
        public SearchHotelPicture Command { get; set; }
        public List<ViewHotelPicture> List { get; set; }

        public IndexModel(IHotelPictureApplication hotelPictureApplication, IHotelApplication hotelApplication)
        {
            this.hotelApplication = hotelApplication;
            this.hotelPictureApplication = hotelPictureApplication;
        }
        #endregion

        #region Search
        [NeedsPermission(((int)HotelPermissions.HotelPicture.List))]
        public void OnGet(SearchHotelPicture command)
        {
            Hotels = hotelApplication.GetSelectList();
            List = hotelPictureApplication.Search(command);
        }
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)HotelPermissions.HotelPicture.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = hotelPictureApplication.Remove(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)HotelPermissions.HotelPicture.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = hotelPictureApplication.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
