using ShopSection.Application.Contracts.OrderApp;

namespace ShopSection.Domain.ServiceAgg
{
    public interface ICartFetchService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
