using Common.Application.Contracts;

namespace InteractionSection.Application.Contracts.EmailApp
{
    public class ViewEmail : BaseEfViewModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string RecieverName { get; set; }
        public string RecieverEmail { get; set; }
    }
}
