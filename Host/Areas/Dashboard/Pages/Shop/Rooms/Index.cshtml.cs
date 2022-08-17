using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.RoomApp;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Shop.Rooms
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IRoomApplication roomApplication;
        private readonly IAirlineApplication hotelApplication;

        public SearchRoom Command { get; set; }
        public List<ViewRoom> List { get; set; }
        public List<ViewHotel> Hotels { get; set; }
        [TempData] public string Message { get; set; }

        public IndexModel(IRoomApplication roomApplication, IAirlineApplication hotelApplication)
        {
            this.roomApplication = roomApplication;
            this.hotelApplication = hotelApplication;
        }
        #endregion

        #region Search
        [NeedsPermission(((int)ShopPermissions.Room.List))]
        public void OnGet(SearchRoom command)
        {
            Hotels = hotelApplication.GetSelectList();
            List = roomApplication.Search(command);
        }
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)ShopPermissions.Room.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = roomApplication.Remove(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)ShopPermissions.Room.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = roomApplication.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
