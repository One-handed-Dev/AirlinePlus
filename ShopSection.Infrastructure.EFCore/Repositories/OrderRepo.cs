using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using ShopSection.Domain.OrderAgg;
using Microsoft.EntityFrameworkCore;
using AccountSection.Infrastructure.EFCore;
using ShopSection.Application.Contracts.OrderApp;

namespace ShopSection.Infrastructure.EFCore.Repositories
{
    public sealed class OrderRepo : BaseEfRepo<ViewOrderDetails, SearchOrder, ViewOrder, Order>, IOrderRepo
    {
        #region Init
        private readonly ShopContext context;
        private readonly AccountContext accountContext;

        public OrderRepo(ShopContext context, AccountContext accountContext) : base(context)
        {
            this.context = context;
            this.accountContext = accountContext;
        }
        #endregion

        public double GetTotalPayAmountBy(long id)
        {
            var order = context.Orders.Select(x => new { x.TotalPayAmount, x.Id }).FirstOrDefault(x => x.Id == id);
            return order is not null ? order.TotalPayAmount : 0;
        }

        public ViewOrderDetails GetOrderDetailsBy(long orderId)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order is null) return new();

            var items = new ViewOrderItem().FromList(order.Items).ToList();
            items.ForEach(each =>
            {
                each.TotalPayAmount = each.Price * each.CountOfNights * each.CountOfFlights;

                var room = context.Flights.AsNoTracking().Include(x => x.Airline).SingleOrDefault(x => x.Id == each.FlightId);
                //each.FlightName = $"{room.Shop.Name} - {room.Name}";
            });

            var account = accountContext.Accounts.SingleOrDefault(x => x.Id == order.AccountId);

            ViewOrderDetails result = new()
            {
                Items = items,
                OrderId = order.Id,
                AccountName = account.Fullname,
                TotalPayAmount = order.TotalPayAmount,
                AccountPhoneNumber = account.PhoneNumber,
            };

            return result;
        }

        public sealed override List<ViewOrder> Search(SearchOrder command)
        {
            var accounts = accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).AsNoTracking();

            var query = context.Orders
                .Select(x => new ViewOrder
            {
                Id = x.Id,
                RefId = x.RefId,
                IsPaid = x.IsPaid,
                AccountId = x.AccountId,
                IsRemoved = x.IsRemoved,
                IsCanceled = x.IsCanceled,
                TotalPayAmount = x.TotalPayAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToJalaliFullString(),
            }).AsNoTracking();

            if (command.AccountId > 0) query = query.Where(x => x.AccountId == command.AccountId);

            var queryList = query.OrderByDescending(x => x.Id).ToList();
            queryList.ForEach(each => each.AccountFullName = accounts.FirstOrDefault(x => x.Id == each.AccountId)?.Fullname);

            return queryList;
        }

        public string GetIssueTrackingNoByAccountId(long accountId) => context.Orders.FirstOrDefault(x => x.AccountId == accountId)?.IssueTrackingNo ?? "?";
    }
}
