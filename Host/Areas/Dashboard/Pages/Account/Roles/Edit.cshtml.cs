using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountSection.Application.Contracts.RoleApp;
using AccountSection.Infrastructure.Config.Permissions;
using System.Linq;

namespace Host.Areas.Dashboard.Pages.Account.Roles
{
    public sealed class EditModel : PageModel
    {
        #region Init
        private readonly IRoleApplication roleApplication;
        private readonly IEnumerable<IPermissionExposer> exposers;

        public SaveRole Command;
        public List<SelectListItem> PermissionsList = new();

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            this.exposers = exposers;
            this.roleApplication = roleApplication;
        }
        #endregion

        #region Edit
        [NeedsPermission(((int)AccountPermissions.Role.Edit))]
        public void OnGet(long id)
        {
            Command = roleApplication.GetDetails(id);

            foreach (var exposer in exposers)
            {
                var exposedPermissions = exposer.Expose();

                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };

                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString()) { Group = group };

                        if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;

                        PermissionsList.Add(item);
                    }
                }
            }
        }

        [NeedsPermission(((int)AccountPermissions.Role.Edit))]
        public IActionResult OnPost(SaveRole command)
        {
            roleApplication.Edit(command);
            return RedirectToPage("Index");
        }
        #endregion
    }
}
