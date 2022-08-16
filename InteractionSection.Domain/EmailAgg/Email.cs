using Common.Domain;

namespace InteractionSection.Domain.EmailAgg
{
    public class Email : BaseEfDomainModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string RecieverName { get; set; }
        public string RecieverEmail { get; set; }
    }
}
