using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Application.Contracts.RoleApp;
using AccountSection.Application.Contracts.AccountApp;
using AccountSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Account.Accounts
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IRoleApplication roleApplication;
        private readonly IAccountApplication accountApplication;
        private readonly IAuthenticationService authenticationService;

        public List<ViewRole> Roles { get; set; }
        public SearchAccount Command { get; set; }
        [TempData] public string Message { get; set; }
        public List<ViewAccount> AccountsList { get; set; }

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication, IAuthenticationService authenticationService)
        {
            this.roleApplication = roleApplication;
            this.accountApplication = accountApplication;
            this.authenticationService = authenticationService;
        }
        #endregion

        #region Edit
        [NeedsPermission(((int)AccountPermissions.Account.Edit))]
        public IActionResult OnGetEdit(long id)
        {
            var result = accountApplication.GetDetails(id);
            result.Roles = roleApplication.GetSelectList();

            return Partial("Edit", result);
        }

        [NeedsPermission(((int)AccountPermissions.Account.Edit))]
        public JsonResult OnPostEdit(SaveAccount command) => new(accountApplication.Edit(command));
        #endregion

        #region Search
        [NeedsPermission(((int)AccountPermissions.Account.List))]
        public void OnGet(SearchAccount command)
        {
            Roles = roleApplication.GetSelectList();
            AccountsList = accountApplication.Search(command);
        }
        #endregion

        #region Create
        [NeedsPermission(((int)AccountPermissions.Account.Create))]
        public IActionResult OnGetCreate() => Partial("./Create", new SaveAccount { Roles = roleApplication.GetSelectList() });

        [NeedsPermission(((int)AccountPermissions.Account.Create))]
        public JsonResult OnPostCreate(SaveAccount command) => new(accountApplication.Create(command));

        #endregion

        #region Ban&Unban
        [NeedsPermission(((int)AccountPermissions.Account.Ban))]
        public IActionResult OnGetBan(long id)
        {
            var result = accountApplication.Ban(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(((int)AccountPermissions.Account.Ban))]
        public IActionResult OnGetUnban(long id)
        {
            var result = accountApplication.Unban(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion

        #region ChangeRole
        [NeedsPermission(((int)AccountPermissions.Account.ChangeRole))]
        public IActionResult OnGetChangeRole(long id)
        {
            var oldRoleId = accountApplication.GetDetails(id).RoleId;
            var currentUserId = authenticationService.GetCurrentAccountId();
            var roles = roleApplication.GetSelectList();
            var oldRole = roles.FirstOrDefault(x => x.Id == oldRoleId);
            roles.Remove(oldRole);

            return Partial("ChangeRole", new ChangeRoleDto(id, default, roles, currentUserId));
        }

        [NeedsPermission(((int)AccountPermissions.Account.ChangeRole))]
        public JsonResult OnPostChangeRole(ChangeRoleDto command) => new(accountApplication.ChangeRole(command));
        #endregion

        #region ChangePassword
        [NeedsPermission(((int)AccountPermissions.Account.ChangePassword))]
        public IActionResult OnGetChangePassword(long id) => Partial("ChangePassword", new ChangePasswordDto(id, string.Empty, string.Empty));

        [NeedsPermission(((int)AccountPermissions.Account.ChangePassword))]
        public JsonResult OnPostChangePassword(ChangePasswordDto command) => new(accountApplication.ChangePassword(command));
        #endregion

        #region Remove&Restore
        [NeedsPermission(((int)AccountPermissions.Account.Remove))]
        public IActionResult OnGetRemove(long id)
        {
            var result = accountApplication.Remove(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)AccountPermissions.Account.Restore))]
        public IActionResult OnGetRestore(long id)
        {
            var result = accountApplication.Restore(id);
            if (result.IsSuccedded) return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
