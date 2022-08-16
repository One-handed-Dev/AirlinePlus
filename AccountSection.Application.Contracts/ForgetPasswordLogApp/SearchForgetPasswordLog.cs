using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public class SearchForgetPasswordLog : BaseEfSearchModel
    {
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
        public string PhoneNumber { get; set; }
    }
}
