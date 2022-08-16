using System;
using Common.Application;
using CommentSection.Domain.CommentAgg;
using AccountSection.Domain.AccountAgg;
using CommentSection.Application.Contracts.CommentApp;
using InteractionSection.Application.Contracts.EmailApp;

namespace CommentSection.Application.CommentApp
{
    public sealed class CommentApplication : BaseEfApplication<ICommentRepo, SaveComment, SearchComment, ViewComment, Comment>, ICommentApplication
    {
        #region Init
        private readonly IAccountRepo accountRepo;
        private readonly IEmailApplication emailApplication;

        public CommentApplication(ICommentRepo repo, IEmailApplication emailApplication, IAccountRepo accountRepo) : base(repo)
        {
            this.accountRepo = accountRepo;
            this.emailApplication = emailApplication;
        }
        #endregion

        public TaskResult Cancel(long id)
        {
            TaskResult operation = new();
            var comment = repo.Get(id);

            if (comment is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            comment.Cancel();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult Confirm(long id)
        {
            TaskResult operation = new();
            var comment = repo.Get(id);

            if (comment is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            comment.Confirm();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult BanOwner(long id)
        {
            TaskResult operation = new();
            var comment = repo.Get(id);

            if (comment is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            var owner = accountRepo.Get(comment.OwnerId);
            owner.Ban();
            return operation.ReturnOkOnlyIfCommitted(accountRepo.Save());
        }

        public TaskResult UnbanOwner(long id)
        {
            TaskResult operation = new();
            var comment = repo.Get(id);

            if (comment is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            var owner = accountRepo.Get(comment.OwnerId);
            owner.Unban();
            return operation.ReturnOkOnlyIfCommitted(accountRepo.Save());
        }

        public TaskResult PlaceFeedback(CommentFeedbackDto command)
        {
            TaskResult task = new();
            var target = repo.Get(command.Id);

            if (target is null)
                return task.Failed(TaskResult.Messages.RecordNotFound);

            target.PlaceFeedback(command.Feedback);
            repo.Save();

            return emailApplication.SendCommentFeedback(command.Id);
        }

        public sealed override TaskResult Create(SaveComment command)
        {
            TaskResult operation = new();

            if (repo.Exists(x => x.OwnerId == command.OwnerId && x.Content == command.Content))
                return operation.Failed(TaskResult.Messages.IsSuccessful);

            var @new = new Comment().From(command);

            repo.Create(@new);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public sealed override TaskResult Edit(SaveComment command) => throw new NotImplementedException();
    }
}
