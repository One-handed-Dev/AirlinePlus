namespace WebQuery.Contracts.Shop.ShopPicture
{
    public class QueryShopPicture
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public bool IsRemoved { get; set; }
        public string Picture { get; set; }
    }
}
