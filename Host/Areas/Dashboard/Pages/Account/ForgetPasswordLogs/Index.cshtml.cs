using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Infrastructure.Config.Permissions;
using AccountSection.Application.Contracts.ForgetPasswordLogApp;

namespace Host.Areas.Dashboard.Pages.Account.ForgetPasswordLogs
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IForgetPasswordLogApplication forgetPasswordLogApplication;

        public SearchForgetPasswordLog Command { get; set; }
        public List<ViewForgetPasswordLog> List { get; set; }

        public IndexModel(IForgetPasswordLogApplication forgetPasswordLogApplication) => this.forgetPasswordLogApplication = forgetPasswordLogApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)AccountPermissions.ForgetPasswordLog.List))]
        public void OnGet(SearchForgetPasswordLog command) => List = forgetPasswordLogApplication.Search(command);
        #endregion
    }
}
