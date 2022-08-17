using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelSection.Application.Contracts.OrderApp;
using HotelSection.Infrastructure.Config.Permissions;
using AccountSection.Application.Contracts.AccountApp;

namespace Host.Areas.Dashboard.Pages.Hotel.Orders
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IOrderApplication orderApplication;
        private readonly IAccountApplication accountApplication;

        public SearchOrder Command { get; set; }
        [TempData] public string Message { get; set; }
        public List<ViewOrder> OrdersList { get; set; }
        public List<ViewAccount> AccountsList { get; set; }

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            this.orderApplication = orderApplication;
            this.accountApplication = accountApplication;
        }
        #endregion

        #region Log
        [NeedsPermission(((int)HotelPermissions.Order.Log))]
        public IActionResult OnGetLog(long id)
        {
            var model = orderApplication.GetOrderDetailsBy(id);
            return Partial("Log", model);
        }
        #endregion

        #region Search
        [NeedsPermission(((int)HotelPermissions.Order.List))]
        public void OnGet(SearchOrder command)
        {
            AccountsList = accountApplication.GetSelectList();
            OrdersList = orderApplication.Search(command);
        }
        #endregion

        #region Cancel
        [NeedsPermission(((int)HotelPermissions.Order.Cancel))]
        public IActionResult OnGetCancel(long id)
        {
            orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }
        #endregion

        #region Confirm
        [NeedsPermission(((int)HotelPermissions.Order.Confirm))]
        public IActionResult OnGetConfirm(long id)
        {
            orderApplication.PaymentSucceeded(id, "0");
            return RedirectToPage("./Index");
        }
        #endregion
    }
}