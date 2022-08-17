using System.Collections.Generic;
using ShopSection.Application.Contracts.OrderApp;

namespace ShopSection.Domain.ServiceAgg
{
    public interface ICartCalculateService
    {
        public List<CartItem> FinalMatchCartItemsWithInventory(List<CartItem> cartItems);
    }
}
