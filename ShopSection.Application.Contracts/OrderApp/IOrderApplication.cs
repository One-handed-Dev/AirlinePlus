using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.OrderApp
{
    public interface IOrderApplication : IBaseEfApplication<ViewOrderDetails, SearchOrder, ViewOrder>
    {
        void Cancel(long id);
        long PlaceOrder(Cart cart);
        double GetTotalPayAmountBy(long id);
        ViewOrderDetails GetOrderDetailsBy(long orderId);
        string PaymentSucceeded(long orderId, string refId);
    }
}
