using Common.Domain;
using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace AccountSection.Application.Contracts.AccountPolicyApp
{
    public class AccountPolicy : BaseJsonDomainModel<AccountPolicy>
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ResetPasswordCallbackUrl { get; set; }

        [Range(1, 60, ErrorMessage = ValidationMessages.IsRequired)]
        public int VerifyCodeTimerDurationMinutes { get; set; }

        [Range(1, 60, ErrorMessage = ValidationMessages.IsRequired)]
        public int ForgetPasswordLogHashExpirationMinutes { get; set; }
    }
}
