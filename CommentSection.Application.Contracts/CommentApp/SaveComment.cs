using Common.Application.Contracts;
using System.ComponentModel.DataAnnotations;
using static Common.Application.Strings.Persian;

namespace CommentSection.Application.Contracts.CommentApp
{
    public class SaveComment : BaseEfSaveModel
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(2000, ErrorMessage = ValidationMessages.InvalidLength)]
        public string Content { get; set; }

        public long OwnerId { get; set; }
        public long RecordId { get; set; }
        public string Feedback { get; set; }
    }
}
