using Common.Domain;
using ShopSection.Application.Contracts.OrderApp;

namespace ShopSection.Domain.OrderAgg
{
    public interface IOrderRepo : IBaseEfRepo<ViewOrderDetails, SearchOrder, ViewOrder, Order>
    {
        double GetTotalPayAmountBy(long id);
        ViewOrderDetails GetOrderDetailsBy(long orderId);
        string GetIssueTrackingNoByAccountId(long accountId);
    }
}
