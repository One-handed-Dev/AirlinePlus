using Common.Application.Contracts;
using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public class SaveForgetPasswordLog : BaseEfSaveModel
    {
        [MaxLength(50, ErrorMessage = ValidationMessages.InvalidLength)]
        public string Hash { get; set; }

        [MaxLength(11, ErrorMessage = ValidationMessages.InvalidLength)]
        public string PhoneNumber { get; set; }

        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
    }
}
