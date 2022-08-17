using System;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using AccountSection.Application.Contracts.RoleApp;

namespace Host.Classes
{
    [HtmlTargetElement(Attributes = "permission")]
    public sealed class PermissionTagHelper : TagHelper
    {
        #region Init
        private readonly IRoleApplication roleApplication;
        private readonly IAuthenticationService authenticationService;

        public Enum Permission { get; set; }

        public PermissionTagHelper(IAuthenticationService authenticationService, IRoleApplication roleApplication)
        {
            this.roleApplication = roleApplication;
            this.authenticationService = authenticationService;
        }
        #endregion

        public sealed override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!authenticationService.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }

            var accountRoleId = authenticationService.GetCurrentAccountInfo().RoleId;

            if (!roleApplication.HasPermission(new(accountRoleId, Convert.ToInt32(Permission))))
            {
                output.SuppressOutput();
                return;
            }

            base.Process(context, output);
        }
    }
}
