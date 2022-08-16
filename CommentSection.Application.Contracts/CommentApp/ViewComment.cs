using Common.Domain;
using Common.Application.Contracts;

namespace CommentSection.Application.Contracts.CommentApp
{
    public sealed class ViewComment : BaseEfViewModel, IConfirmable
    {
        public long OwnerId { get; set; }
        public long RecordId { get; set; }
        public string Content { get; set; }
        public string Feedback { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsConfirmed { get; set; }
        public string OwnerName { get; set; }
        public string RecordName { get; set; }
        public bool IsOwnerBanned { get; set; }
    }
}
