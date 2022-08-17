using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.RoomApp;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Hotel.Rooms
{
    public sealed class CreateModel : PageModel
    {
        #region Init
        private readonly IRoomApplication roomApplication;
        private readonly IHotelApplication hotelApplication;

        public SaveRoom Command { get; set; }
        public List<ViewHotel> Hotels { get; set; }

        public CreateModel(IHotelApplication hotelApplication, IRoomApplication roomApplication)
        {
            this.roomApplication = roomApplication;
            this.hotelApplication = hotelApplication;
        }
        #endregion

        #region Create
        [NeedsPermission(((int)HotelPermissions.Room.Create))]
        public void OnGet() => Hotels = hotelApplication.GetSelectList();

        [NeedsPermission(((int)HotelPermissions.Room.Create))]
        public IActionResult OnPost(SaveRoom command)
        {
            command.CountOfAvailableRoom = command.CountOfThisRoomTypeInHotel;
            roomApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
