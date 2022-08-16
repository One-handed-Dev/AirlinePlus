using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public class ViewForgetPasswordLog : BaseEfViewModel
    {
        public bool IsUsed { get; set; }
        public string Hash { get; set; }
        public bool IsExpired { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountName { get; set; }
    }
}
