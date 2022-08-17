using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace InteractionSection.Application.Contracts.EmailApp
{
    public class EmailDto
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<MailboxAddress> To { get; set; }

        public EmailDto(List<string> to, string subject, string content)
        {
            Subject = subject;
            Content = content;

            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
        }
    }
}
