using Common.Domain;
using System.Collections.Generic;

namespace ShopSection.Domain.OrderAgg
{
    public sealed class Order : BaseEfDomainModel
    {
        #region Props
        public string RefId { get; set; }
        public long AccountId { get; set; }
        public bool IsPaid { get; set; } = false;
        public double TotalPayAmount { get; set; }
        public string IssueTrackingNo { get; set; }
        public bool IsCanceled { get; set; } = false;
        public List<OrderItem> Items { get; set; } = new();

        public Order() { }

        public Order(long accountId, double totalPayAmount)
        {
            AccountId = accountId;
            TotalPayAmount = totalPayAmount;
        }
        #endregion

        public void Cancel() => IsCanceled = true;

        public void PaymentSucceeded(string refId)
        {
            IsPaid = true;
            if (refId != "0") RefId = refId;
        }

        public void Add(OrderItem item) => Items.Add(item);

        public void SetIssueTrackingNo(string number) => IssueTrackingNo = number;
    }
}
