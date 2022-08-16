using System;
using System.Linq;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using HotelSection.Infrastructure.EFCore;
using AccountSection.Infrastructure.EFCore;
using CommentSection.Infrastructure.EFCore;
using HotelSection.Application.Contracts.HotelApp;
using HotelSection.Application.Contracts.OrderApp;
using InteractionSection.Domain.DashboardStatisticAgg;
using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace InteractionSection.Infrastructure.EFCore.Repositories
{
    public class DashboardStatisticRepo : IDashboardStatisticRepo
    {
        #region Init
        private readonly HotelContext hotelContext;
        private readonly AccountContext accountContext;
        private readonly CommentContext commentContext;

        public DashboardStatisticRepo(HotelContext hotelContext, AccountContext accountContext, CommentContext commentContext)
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
            statistic.FiveRecentlyHotelDefinition = new ViewHotel().FromList(hotelContext.Hotels.AsNoTracking().OrderByDescending(x => x.CreationDate).Take(5)).ToList();
            statistic.LastMonthReservationsCount = hotelContext.Orders.AsNoTracking().OrderByDescending(x => x.Id).Count(x => x.CreationDate.Month == DateTime.Now.Month);
            statistic.LastMonthEnrollmentsCount = accountContext.Accounts.AsNoTracking().OrderByDescending(x => x.Id).Count(x => x.CreationDate.Month == DateTime.Now.Month);
            statistic.FiveRecentlyHotelReservations = new ViewOrder().FromList(hotelContext.Orders.AsNoTracking().OrderByDescending(x => x.CreationDate).Take(5), Projection.DateTimeMode.BothDateAndTime).ToList();

            statistic.FiveRecentlyHotelReservations.ForEach(each => each.AccountFullName = accountContext.Accounts.SingleOrDefault(x => x.Id == each.AccountId)?.Fullname ?? "?");
            return statistic;
        }
    }
}
