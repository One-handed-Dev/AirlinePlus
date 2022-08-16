using System.Collections.Generic;
using ShopSection.Application.Contracts.OrderApp;

namespace WebQuery.Contracts.Shop.Shop
{
    public interface IShopQuery
    {
        QueryShop[] GetAll();
        QueryShop GetDetails(long id);
        QueryShop[] Search(string query);
        List<CartItem> FetchData(List<CartItem> cartItems);
        QueryShop[] GetSameCityShops(string city, long id);
    }
}
