using System.Collections.Generic;
using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.OrderApp
{
    public sealed class ViewOrderDetails : BaseEfSaveModel
    {
        public long OrderId { get; set; }
        public string AccountName { get; set; }
        public double TotalPayAmount { get; set; }
        public List<ViewOrderItem> Items { get; set; }
        public string AccountPhoneNumber { get; set; }
    }
}
