using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Application.Contracts.RoleApp;
using AccountSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        #region Init
        private readonly IRoleApplication roleApplication;
        private readonly IEnumerable<IPermissionExposer> exposers;

        public SaveRole Command;
        public List<SelectListItem> PermissionsList = new();

        public CreateModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            this.exposers = exposers;
            this.roleApplication = roleApplication;
        }
        #endregion

        #region Create
        [NeedsPermission(((int)AccountPermissions.Role.Create))]
        public void OnGet()
        {
            foreach (var exposer in exposers)
            {
                var exposedPermissions = exposer.Expose();

                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };

                    foreach (var permission in value)
                        PermissionsList.Add(new SelectListItem(permission.Name, permission.Code.ToString()) { Group = group });
                }
            }
        }

        [NeedsPermission(((int)AccountPermissions.Role.Create))]
        public IActionResult OnPost(SaveRole command)
        {
            roleApplication.Create(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
