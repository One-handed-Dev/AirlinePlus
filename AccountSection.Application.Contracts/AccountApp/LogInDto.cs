using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.AccountApp
{
    public sealed record LogInDto
        (
            long Id,
            long RoleId,
            [Required(ErrorMessage = ValidationMessages.IsRequired)] string Password,
            [Required(ErrorMessage = ValidationMessages.IsRequired)] string PhoneNumber
        );
}
