using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.AccountApp
{
    public record VerifyDto([Required(ErrorMessage = ValidationMessages.IsRequired)] string VerificationCode);
}
