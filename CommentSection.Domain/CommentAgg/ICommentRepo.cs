using Common.Domain;
using CommentSection.Application.Contracts.CommentApp;

namespace CommentSection.Domain.CommentAgg
{
    public interface ICommentRepo :
        IBaseEfRepo<SaveComment, SearchComment, ViewComment, Comment>
    {
    }
}
