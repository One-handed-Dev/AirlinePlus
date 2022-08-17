using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.RoomApp;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Hotel.Rooms
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IRoomApplication roomApplication;
        private readonly IHotelApplication hotelApplication;

        public SaveRoom Command { get; set; }
        public List<ViewHotel> Hotels { get; set; }

        public EditModel(IHotelApplication hotelApplication, IRoomApplication roomApplication)
        {
            this.roomApplication = roomApplication;
            this.hotelApplication = hotelApplication;
        }
        #endregion

        #region Edit
        [NeedsPermission(((int)HotelPermissions.Room.Edit))]
        public void OnGet(long id)
        {
            Command = roomApplication.GetDetails(id);
            Hotels = hotelApplication.GetSelectList();
        }

        [NeedsPermission(((int)HotelPermissions.Room.Edit))]
        public IActionResult OnPost(SaveRoom command)
        {
            roomApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
