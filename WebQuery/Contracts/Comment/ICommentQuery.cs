using Common.Presentation;
using System.Collections.Generic;

namespace WebQuery.Contracts.Comment
{
    public interface ICommentQuery
    {
        void RemoveAll(long ownerId);
        void Remove(long ownerId, long commentId);
        List<QueryComment> GetHotelComments(long hotelId);
        List<QueryComment> GetUserAllComments(long ownerId);
    }
}
