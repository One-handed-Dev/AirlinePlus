using System.Linq;
using Common.Application;
using System.Collections.Generic;
using HotelSection.Domain.OrderAgg;
using Microsoft.EntityFrameworkCore;
using WebQuery.Contracts.Hotel.Order;
using HotelSection.Infrastructure.EFCore;
using static Common.Application.Projection;

namespace WebQuery.Query
{
    public sealed class OrderQuery : IOrderQuery
    {
        #region Init
        private readonly HotelContext context;

        public OrderQuery(HotelContext context) => this.context = context;
        #endregion

        private List<QueryOrderItem> Map(List<OrderItem> items)
        {
            var mapped = new QueryOrderItem().FromList(items).ToList();

            mapped.ForEach(each =>
            {
                var room = context.Rooms.AsNoTracking().SingleOrDefault(x => x.Id == each.RoomId);

                if (room is not null)
                {
                    var hotel = context.Hotels.SingleOrDefault(x => x.Id == room.HotelId);

                    each.RoomId = room.Id;
                    each.HotelId = hotel.Id;
                    each.Price = room.Price;
                    each.Picture = hotel.Picture;
                    each.Name = $"{hotel.Name} - {room.Name}";
                    each.TotalItemPrice = each.Price * each.CountOfNights * each.CountOfRooms;
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
