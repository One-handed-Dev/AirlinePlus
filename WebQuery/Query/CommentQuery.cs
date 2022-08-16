using System;
using System.Linq;
using Common.Application;
using Common.Presentation;
using System.Collections.Generic;
using WebQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using ShopSection.Infrastructure.EFCore;
using AccountSection.Infrastructure.EFCore;
using CommentSection.Infrastructure.EFCore;

namespace WebQuery.Query
{
    public sealed class CommentQuery : ICommentQuery
    {
        #region Init
        private readonly ShopContext hotelContext;
        private readonly AccountContext accountContext;
        private readonly CommentContext commentContext;

        public CommentQuery(CommentContext commentContext, ShopContext hotelContext, AccountContext accountContext)
        {
            this.hotelContext = hotelContext;
            this.commentContext = commentContext;
            this.accountContext = accountContext;
        }
        #endregion

        public void RemoveAll(long ownerId)
        {
            var target = commentContext.Comments.Where(x => x.OwnerId == ownerId).ToList();

            if (target is not null)
                target.ForEach(x => x.Remove());

            commentContext.SaveChanges();
        }

        public void Remove(long ownerId, long commentId)
        {
            var target = commentContext.Comments.FirstOrDefault(x => x.OwnerId == ownerId && x.Id == commentId);

            if (target is not null)
                target.Remove();

            commentContext.SaveChanges();
        }

        public List<QueryComment> GetShopComments(long hotelId)
        {
            Random random = new();

            var comments = new QueryComment()
                .FromList(commentContext.Comments
                .AsNoTracking()
                .Where(x => x.IsConfirmed)
                .Where(x => x.RecordId == hotelId)
                .Where(x => !x.IsRemoved)
                .OrderByDescending(x => x.CreationDate))
                .ToList();

            comments.ForEach(each =>
            {
                var owner = accountContext.Accounts.FirstOrDefault(x => x.Id == each.OwnerId);

                #region Fetch Picture
                var ownerPicture = owner?.ProfilePicture;
                each.ProfilePicture = string.IsNullOrWhiteSpace((string)ownerPicture) ? $"ProfilePictures/no-picture-{random.Next((int)1, (int)5)}.png" : ownerPicture;
                #endregion

                #region Fetch Name
                var ownerName = owner?.Fullname;
                each.OwnerName = string.IsNullOrWhiteSpace((string)ownerName) ? "کاربر رویش مارکت" : ownerName;
                #endregion

                #region Fetch Order Status
                var ownerOrders = hotelContext.Orders.Where(x => x.AccountId == each.OwnerId).ToList();

                foreach (var order in ownerOrders)
                    if (order.Items.Any(x => x.RoomId == each.ShopId))
                    {
                        each.IsOwnerBuyer = true;
                        break;
                    }
                #endregion
            });

            return comments ?? new();
        }

        public List<QueryComment> GetUserAllComments(long ownerId)
        {
            var query = new QueryComment().FromList(commentContext.Comments.Where(x => x.OwnerId == ownerId && !x.IsRemoved).OrderByDescending(x => x.Id).AsNoTracking()).ToList();

            query.ForEach(each =>
            {
                var targetProduct = hotelContext.Shops.AsNoTracking().FirstOrDefault(x => x.Id == each.ShopId);
                if (targetProduct is not null) each.TargetName = targetProduct.Name;
            });

            return query;
        }
    }
}
