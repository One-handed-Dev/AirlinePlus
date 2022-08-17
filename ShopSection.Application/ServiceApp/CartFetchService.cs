using ShopSection.Application.Contracts.OrderApp;
using ShopSection.Domain.ServiceAgg;

namespace ShopSection.Application.ServiceApp
{
    public sealed class CartFetchService : ICartFetchService
    {
        public Cart Cart { get; set; }

        public Cart Get() => Cart;
        public void Set(Cart cart) => Cart = cart;
    }
}
