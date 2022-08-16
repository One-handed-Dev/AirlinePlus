using Common.Infrastructure;
using System.Collections.Generic;
using Common.Application.Contracts;
using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.RoleApp
{
    public class SaveRole : BaseEfSaveModel
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.InvalidLength)]
        public string Name { get; set; }

        public List<int> Permissions { get; set; }
        public bool IsAllowedToEnterDashboard { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}
