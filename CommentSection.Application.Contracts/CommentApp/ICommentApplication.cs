using Common.Application;
using Common.Application.Contracts;

namespace CommentSection.Application.Contracts.CommentApp
{
    public interface ICommentApplication : IBaseEfApplication<SaveComment, SearchComment, ViewComment>
    {
        TaskResult Cancel(long id);
        TaskResult Confirm(long id);
        TaskResult BanOwner(long id);
        TaskResult UnbanOwner(long id);
        TaskResult PlaceFeedback(CommentFeedbackDto command);
    }
}
