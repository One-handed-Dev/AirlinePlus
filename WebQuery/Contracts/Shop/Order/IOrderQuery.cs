using System.Collections.Generic;

namespace WebQuery.Contracts.Shop.Order
{
    public interface IOrderQuery
    {
        List<QueryOrder> GetUserAllOrdersBy(long accountId);
    }
}
