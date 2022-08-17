using System;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using AccountSection.Application.Contracts.RoleApp;

namespace Host.Classes
{
    public sealed class SecurityPageFilter : IPageFilter
    {
        #region Init
        private readonly IRoleApplication roleApplication;
        private readonly IAuthenticationService authenticationService;

        public SecurityPageFilter(IAuthenticationService authenticationService, IRoleApplication roleApplication)
        {
            this.roleApplication = roleApplication;
            this.authenticationService = authenticationService;
        }
        #endregion

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var allAttributes = Array.Empty<object>();

            if (context.HandlerMethod is not null)
                allAttributes =
                    context.HandlerMethod.MethodInfo.GetCustomAttributes(
                        typeof(NeedsPermissionAttribute), false);

            NeedsPermissionAttribute? permissionHandler;

            if (allAttributes.Length > 0)
                permissionHandler = allAttributes[0] as NeedsPermissionAttribute;

            else return;

            if (permissionHandler is null) return;

            var loggedInAccountInfo = authenticationService.GetCurrentAccountInfo();

            if (!roleApplication.HasPermission(new(loggedInAccountInfo.RoleId, permissionHandler.Permission)))
                context.HttpContext.Response.Redirect("/AccessDenied");
        }
    }
}
