using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CommentSection.Domain.CommentAgg;
using ShopSection.Infrastructure.EFCore;
using AccountSection.Infrastructure.EFCore;
using CommentSection.Application.Contracts.CommentApp;

namespace CommentSection.Infrastructure.EFCore.Repositories
{
    public sealed class CommentRepo :
        BaseEfRepo<SaveComment, SearchComment, ViewComment, Comment>, ICommentRepo
    {
        #region Init
        private readonly ShopContext shopContext;
        private readonly CommentContext commentContext;
        private readonly AccountContext accountContext;

        public CommentRepo(CommentContext commentContext, AccountContext accountContext, ShopContext shopContext) : base(commentContext)
        {
            this.shopContext = shopContext;
            this.commentContext = commentContext;
            this.accountContext = accountContext;
        }
        #endregion

        public sealed override List<ViewComment> Search(SearchComment command)
        {
            var query = Projection.FromList(new ViewComment(), commentContext.Comments.AsNoTracking(), Projection.DateTimeMode.BothDateAndTime);

            var list = query.OrderByDescending(x => x.Id).ToList();
            foreach (var each in list)
            {
                var accountName = accountContext.Accounts.FirstOrDefault(x => x.Id == each.OwnerId)?.Fullname;
                each.OwnerName = (string.IsNullOrWhiteSpace(accountName) ? "بدون نام" : accountName);

                each.RecordName = shopContext.Airlines.AsNoTracking().FirstOrDefault(x => x.Id == each.RecordId)?.Name;
            }

            if (!string.IsNullOrWhiteSpace(command.OwnerName)) list = list.Where(x => x.OwnerName.Contains(command.OwnerName)).ToList();

            return list;
        }
    }
}
