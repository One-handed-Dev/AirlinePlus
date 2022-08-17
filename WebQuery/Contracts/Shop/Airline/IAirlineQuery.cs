using System.Collections.Generic;
using ShopSection.Application.Contracts.OrderApp;

namespace WebQuery.Contracts.Shop.Airline
{
    public interface IAirlineQuery
    {
        QueryAirline[] GetAll();
        QueryAirline GetDetails(long id);
        QueryAirline[] Search(string query);
        List<CartItem> FetchData(List<CartItem> cartItems);
    }
}
