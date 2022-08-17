using AccountSection.Application.Contracts.RoleApp;
using Common.Application.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.AccountApp
{
    public class SaveAccount : BaseEfSaveModel
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PhoneNumber { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get; set; }

        public bool IsRemoved { get; set; }
        public List<ViewRole> Roles { get; set; }
        public string VerificationCode { get; set; }
    }
}
