using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public record ForgetPasswordDto(
            [Required(ErrorMessage = ValidationMessages.IsRequired)]
            [StringLength(11, MinimumLength = 11, ErrorMessage = ValidationMessages.InvalidLength)]
            string PhoneNumber
        );
}
