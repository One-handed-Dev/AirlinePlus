using Common.Domain;

namespace CommentSection.Domain.CommentAgg
{
    public sealed class Comment : BaseEfDomainModel, IFullConfirmable
    {
        public long OwnerId { get; set; }
        public long RecordId { get; set; }
        public string Content { get; set; }
        public string Feedback { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }

        public void Cancel() => IsCanceled = true;

        public void Confirm() => IsConfirmed = true;

        public void PlaceFeedback(string feedback)
        {
            IsAnswered = true;
            Feedback = feedback;
        }
    }
}
