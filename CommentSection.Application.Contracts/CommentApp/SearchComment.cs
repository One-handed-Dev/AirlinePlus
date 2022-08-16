using Common.Application.Contracts;

namespace CommentSection.Application.Contracts.CommentApp
{
    public sealed class SearchComment : BaseEfSearchModel
    {
        public bool IsAnswered { get; set; }
        public bool IsConfirmed { get; set; }
        public string OwnerName { get; set; }
    }
}
