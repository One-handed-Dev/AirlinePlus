using System;
using System.Linq;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using ShopSection.Infrastructure.EFCore;
using AccountSection.Infrastructure.EFCore;
using CommentSection.Infrastructure.EFCore;
using ShopSection.Application.Contracts.ShopApp;
using ShopSection.Application.Contracts.OrderApp;
using InteractionSection.Domain.DashboardStatisticAgg;
using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace InteractionSection.Infrastructure.EFCore.Repositories
{
    public class DashboardStatisticRepo : IDashboardStatisticRepo
    {
        #region Init
        private readonly ShopContext hotelContext;
        private readonly AccountContext accountContext;
        private readonly CommentContext commentContext;

        public DashboardStatisticRepo(ShopContext hotelContext, AccountContext accountContext, CommentContext commentContext)
        {
            this.hotelContext = hotelContext;
            this.accountContext = accountContext;
            this.commentContext = commentContext;
        }
        #endregion

        public DashboardStatistic Get()
        {
            DashboardStatistic statistic = new();
            statistic.UnreadCommentsCount = commentContext.Comments.AsNoTracking().Count(x => !x.IsAnswered && !x.IsCanceled && !x.IsConfirmed);
            statistic.FiveRecentlyShopDefinition = new ViewShop().FromList(hotelContext.Shops.AsNoTracking().OrderByDescending(x => x.CreationDate).Take(5)).ToList();
            statistic.LastMonthReservationsCount = hotelContext.Orders.AsNoTracking().OrderByDescending(x => x.Id).Count(x => x.CreationDate.Month == DateTime.Now.Month);
            statistic.LastMonthEnrollmentsCount = accountContext.Accounts.AsNoTracking().OrderByDescending(x => x.Id).Count(x => x.CreationDate.Month == DateTime.Now.Month);
            statistic.FiveRecentlyShopReservations = new ViewOrder().FromList(hotelContext.Orders.AsNoTracking().OrderByDescending(x => x.CreationDate).Take(5), Projection.DateTimeMode.BothDateAndTime).ToList();

            statistic.FiveRecentlyShopReservations.ForEach(each => each.AccountFullName = accountContext.Accounts.SingleOrDefault(x => x.Id == each.AccountId)?.Fullname ?? "?");
            return statistic;
        }
    }
}
