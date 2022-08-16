using HotelSection.Domain.OrderAgg;
using System.Collections.Generic;

namespace WebQuery.Contracts.Hotel.Order
{
    public sealed class QueryOrder
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string CreationDate { get; set; }
        public double TotalPayAmount { get; set; }
        public string IssueTrackingNo { get; set; }
        public List<QueryOrderItem> Items { get; set; } = new();
        public List<OrderItem> NotMappedItems { get; set; } = new();
    }
}
