using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Application.Contracts.RoleApp;
using AccountSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Account.Roles
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IRoleApplication roleApplication;

        public SearchRole Command { get; set; }
        public List<ViewRole> RolesList { get; set; }
        [TempData] public string Message { get; set; }

        public IndexModel(IRoleApplication roleApplication) => this.roleApplication = roleApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)AccountPermissions.Role.List))]
        public void OnGet(SearchRole command) => RolesList = roleApplication.Search(command);
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)AccountPermissions.Role.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = roleApplication.Remove(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)AccountPermissions.Role.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = roleApplication.Restore(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
