using Common.Application;
using Microsoft.EntityFrameworkCore;
using ShopSection.Domain.OrderAgg;
using ShopSection.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;
using WebQuery.Contracts.Shop.Order;
using static Common.Application.Projection;

namespace WebQuery.Query
{
    public sealed class OrderQuery : IOrderQuery
    {
        #region Init
        private readonly ShopContext context;

        public OrderQuery(ShopContext context) => this.context = context;
        #endregion

        private List<QueryOrderItem> Map(List<OrderItem> items)
        {
            var mapped = new QueryOrderItem().FromList(items).ToList();

            mapped.ForEach(each =>
            {
                var flight = context.Flights.AsNoTracking().SingleOrDefault(x => x.Id == each.FlightId);

                if (flight is not null)
                {
                    var airline = context.Flights.SingleOrDefault(x => x.Id == flight.AirlineId);

                    each.FlightId = flight.Id;
                    each.ShopId = airline.Id;
                    each.Price = flight.Price;
                    //each.Picture = airline.Picture;
                    //each.Name = $"{airline.Name} - {flight.Name}";
                    each.TotalItemPrice = each.Price * each.CountOfNights * each.CountOfFlights;
                }
            });

            return mapped;
        }

        public List<QueryOrder> GetUserAllOrdersBy(long accountId)
        {
            var query = context.Orders
                .AsNoTracking()
                .Where(x => x.AccountId == accountId)
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new QueryOrder()
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    NotMappedItems = x.Items,
                    TotalPayAmount = x.TotalPayAmount,
                    IssueTrackingNo = x.IssueTrackingNo,
                    CreationDate = x.CreationDate.ToJalaliFullString(),
                })
                .ToList();

            query.ForEach(each =>
            {
                each.Items = Map(each.NotMappedItems);
            });

            return query;
        }
    }
}
