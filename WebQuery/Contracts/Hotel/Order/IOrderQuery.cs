using System.Collections.Generic;

namespace WebQuery.Contracts.Hotel.Order
{
    public interface IOrderQuery
    {
        List<QueryOrder> GetUserAllOrdersBy(long accountId);
    }
}
