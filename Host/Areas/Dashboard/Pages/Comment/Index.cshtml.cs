using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommentSection.Application.Contracts.CommentApp;
using CommentSection.Infrastructure.Config.Permissions;
using AccountSection.Infrastructure.Config.Permissions;

namespace Host.Areas.Dashboard.Pages.Comment
{
    public sealed class IndexModel : PageModel
    {
        #region Init
        private readonly ICommentApplication commentApplication;

        public SearchComment Command { get; set; }
        [TempData] public string Message { get; set; }
        public List<ViewComment> CommentsList { get; set; }

        public IndexModel(ICommentApplication commentApplication) => this.commentApplication = commentApplication;
        #endregion

        #region Search
        [NeedsPermission(((int)CommentPermissions.Comment.List))]
        public void OnGet(SearchComment command) => CommentsList = commentApplication.Search(command);
        #endregion

        #region Answer
        [NeedsPermission(((int)CommentPermissions.Comment.Send))]
        public IActionResult OnGetPlaceAnswer(long id)
        {
            var comment = commentApplication.GetDetails(id);

            if (comment is null) return default;

            var feedbackDto = new CommentFeedbackDto(comment.Id, string.Empty, comment.Feedback);

            return Partial("PlaceAnswer", feedbackDto);
        }

        [NeedsPermission(((int)CommentPermissions.Comment.Send))]
        public JsonResult OnPostPlaceAnswer(CommentFeedbackDto command) => new(commentApplication.PlaceFeedback(command));
        #endregion

        #region Ban&Unban
        [NeedsPermission(((int)AccountPermissions.Account.Edit))]
        public IActionResult OnGetBanOwner(long id)
        {
            var result = commentApplication.BanOwner(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        [NeedsPermission(((int)AccountPermissions.Account.Edit))]
        public IActionResult OnGetUnbanOwner(long id)
        {
            var result = commentApplication.UnbanOwner(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion

        #region Confirm&Cancel&SendFeedback
        [NeedsPermission(((int)CommentPermissions.Comment.Confirm))]
        public IActionResult OnGetConfirm(long id)
        {
            var result = commentApplication.Confirm(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(((int)CommentPermissions.Comment.Cancel))]
        public IActionResult OnGetCancel(long id)
        {
            var result = commentApplication.Cancel(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
