using Common.Application;
using System.Collections.Generic;
using ShopSection.Application.Contracts.OrderApp;
using ShopSection.Domain.ServiceAgg;

namespace ShopSection.Application.ServiceApp
{
    public sealed class CartCalculateService : ICartCalculateService
    {
        #region Init
        #endregion

        public List<CartItem> FinalMatchCartItemsWithInventory(List<CartItem> cartItems)
        {
            List<CartItem> matchedList = new();

            cartItems.ForEach(each =>
            {
                if (each.CountOfFlights > 0)
                {
                    var matched = new CartItem().From(each);
                    /*var inventory = inventoryRepo.GetByProductId(each.Id);

                    if (inventory is not null)
                    {
                        matched.UnitPrice = inventory.UnitPrice;
                        matchedList.Add(matched);
                    }*/
                }
            });

            return matchedList;
        }
    }
}
