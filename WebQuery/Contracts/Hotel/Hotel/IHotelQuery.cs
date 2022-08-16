using System.Collections.Generic;
using HotelSection.Application.Contracts.OrderApp;

namespace WebQuery.Contracts.Hotel.Hotel
{
    public interface IHotelQuery
    {
        QueryHotel[] GetAll();
        QueryHotel GetDetails(long id);
        QueryHotel[] Search(string query);
        List<CartItem> FetchData(List<CartItem> cartItems);
        QueryHotel[] GetSameCityHotels(string city, long id);
    }
}
