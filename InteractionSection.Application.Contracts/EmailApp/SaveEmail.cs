using Common.Application.Contracts;

namespace InteractionSection.Application.Contracts.EmailApp
{
    public class SaveEmail : BaseEfSaveModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string RecieverName { get; set; }
        public string RecieverEmail { get; set; }

        public SaveEmail() { }

        public SaveEmail(string subject, string message, string recieverName, string recieverEmail)
        {
            Subject = subject;
            Message = message;
            RecieverName = recieverName;
            RecieverEmail = recieverEmail;
        }
    }
}
