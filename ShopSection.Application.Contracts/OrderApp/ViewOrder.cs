using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.OrderApp
{
    public sealed class ViewOrder : BaseEfViewModel
    {
        public string RefId { get; set; }
        public long AccountId { get; set; }
        public bool IsPaid { get; set; } = false;
        public double TotalPayAmount { get; set; }
        public string AccountFullName { get; set; }
        public string IssueTrackingNo { get; set; }
        public bool IsCanceled { get; set; } = false;
    }
}
