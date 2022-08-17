using CommentSection.Application.Contracts.CommentApp;
using Common.Domain;

namespace CommentSection.Domain.CommentAgg
{
    public interface ICommentRepo :
        IBaseEfRepo<SaveComment, SearchComment, ViewComment, Comment>
    {
    }
}
