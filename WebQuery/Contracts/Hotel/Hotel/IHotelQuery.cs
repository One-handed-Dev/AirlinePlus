using System.Collections.Generic;
using ShopSection.Application.Contracts.OrderApp;

namespace WebQuery.Contracts.Shop.Shop
{
    public interface IAirlineQuery
    {
        QueryShop[] GetAll();
        QueryShop GetDetails(long id);
        QueryShop[] Search(string query);
        List<CartItem> FetchData(List<CartItem> cartItems);
    }
}
