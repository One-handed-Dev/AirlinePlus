using Common.Application.Contracts;
using Common.Domain;

namespace Common.Presentation
{
    public sealed class QueryComment : BaseEfViewModel, IConfirmable
    {
        public long OwnerId { get; set; }
        public long? ShopId { get; set; }
        public string Content { get; set; }
        public bool IsCanceled { get; set; }
        public string OwnerName { get; set; }
        public bool IsConfirmed { get; set; }
        public string TargetName { get; set; }
        public string TargetSlug { get; set; }
        public bool IsOwnerBuyer { get; set; }
        public string ProfilePicture { get; set; }
    }
}
