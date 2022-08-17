using Common.Application.Contracts;

namespace ShopSection.Application.Contracts.OrderApp
{
    public sealed class SearchOrder : BaseEfSearchModel
    {
        public long AccountId { get; set; }
        public bool IsCanceled { get; set; }
    }
}
