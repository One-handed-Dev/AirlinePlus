using Common.Application;
using Common.Application.Contracts;

namespace InteractionSection.Application.Contracts.EmailApp
{
    public interface IEmailApplication : IBaseEfApplication<SaveEmail, SearchEmail, ViewEmail>
    {
        TaskResult Send(EmailDto command);
        TaskResult SendOrderNotif(long orderId);
        TaskResult SendLogInNotif(long accountId);
        TaskResult SendSignUpNotif(string phoneNumber);
        TaskResult SendCommentFeedback(long commentId);
        TaskResult SendRecoverPasswordLink(string phoneNumber);
    }
}
