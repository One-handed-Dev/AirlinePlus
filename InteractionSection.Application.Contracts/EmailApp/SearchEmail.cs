using Common.Application.Contracts;

namespace InteractionSection.Application.Contracts.EmailApp
{
    public class SearchEmail : BaseEfSearchModel
    {
        public string Subject { get; set; }
        public string RecieverName { get; set; }
        public string RecieverEmail { get; set; }
    }
}
