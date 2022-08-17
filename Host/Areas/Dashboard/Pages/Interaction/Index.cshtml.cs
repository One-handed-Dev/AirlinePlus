using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InteractionSection.Application.Contracts.EmailApp;
using InteractionSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Interaction
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly IEmailApplication emailApplication;

        public SearchEmail Command { get; set; }
        public List<ViewEmail> List { get; set; }

        public IndexModel(IEmailApplication emailApplication) => this.emailApplication = emailApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)InteractionPermissions.Email.List))]
        public void OnGet(SearchEmail command) => List = emailApplication.Search(command);
        #endregion
    }
}
