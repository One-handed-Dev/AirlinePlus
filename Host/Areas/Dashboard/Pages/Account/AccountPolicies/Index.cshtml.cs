using Microsoft.AspNetCore.Mvc;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Infrastructure.Config.Permissions;
using AccountSection.Application.Contracts.AccountPolicyApp;

namespace Host.Areas.Dashboard.Pages.Account.AccountPolicies
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IAccountPolicyApplication accountPolicyApplication;

        public AccountPolicy Command;

        public IndexModel(IAccountPolicyApplication accountPolicyApplication)
        {
            this.accountPolicyApplication = accountPolicyApplication;
        }
        #endregion

        #region Edit
        [NeedsPermission(((int)AccountPermissions.AccountPolicy.Edit))]
        public void OnGet()
        {
            Command = accountPolicyApplication.Get();
        }

        [NeedsPermission(((int)AccountPermissions.AccountPolicy.Edit))]
        public IActionResult OnPost(AccountPolicy command)
        {
            accountPolicyApplication.Set(command);
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
