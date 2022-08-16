using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public class RecoverPasswordDto
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Hash { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)] 
        public string NewPassword { get; set; }
    }
}
