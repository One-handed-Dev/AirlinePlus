using AccountSection.Application.Contracts.ForgetPasswordLogApp;
using AccountSection.Domain.AccountPolicyAgg;
using AccountSection.Domain.ForgetPasswordLogAgg;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccountSection.Infrastructure.EFCore.Repositories
{
    public class ForgetPasswordLogRepo : BaseEfRepo<SaveForgetPasswordLog, SearchForgetPasswordLog, ViewForgetPasswordLog, ForgetPasswordLog>, IForgetPasswordLogRepo
    {
        #region Init
        private readonly AccountContext context;
        private readonly IAccountPolicyRepo accountPolicyRepo;

        public ForgetPasswordLogRepo(AccountContext context, IAccountPolicyRepo accountPolicyRepo) : base(context)
        {
            this.context = context;
            this.accountPolicyRepo = accountPolicyRepo;
        }
        #endregion

        public TaskResult Use(string hash)
        {
            TaskResult task = new();
            var target = context.ForgetPasswordLogs.FirstOrDefault(x => x.Hash == hash);

            if (target is null)
                task.Failed(TaskResult.Messages.RecordNotFound);

            target.Use();
            return task.ReturnOkOnlyIfCommitted(Save());
        }

        public TaskResult UpdateExpirationStatus()
        {
            var now = DateTime.Now;
            TaskResult task = new();
            var policy = accountPolicyRepo.Get();

            if (policy is null) return task.Failed();

            var query = context.ForgetPasswordLogs.Where(x => !x.IsExpired);
            var validationDuration = policy.ForgetPasswordLogHashExpirationMinutes;

            foreach (var each in query)
            {
                if (each.CreationDate.AddMinutes(validationDuration) < now)
                    each.IsExpired = true;
            }

            return task.ReturnOkOnlyIfCommitted(Save());
        }

        public string GetValidHash(Expression<Func<ForgetPasswordLog, bool>> expression)
        {
            UpdateExpirationStatus();
            return context.ForgetPasswordLogs.Where(x => !x.IsUsed && !x.IsExpired).OrderBy(x => x.Id).LastOrDefault(expression)?.Hash ?? null;
        }

        public override List<ViewForgetPasswordLog> Search(SearchForgetPasswordLog command)
        {
            UpdateExpirationStatus();

            var query = new ViewForgetPasswordLog().FromList(context.ForgetPasswordLogs.AsNoTracking(), Projection.DateTimeMode.BothDateAndTime);

            if (command.IsUsed) query = query.Where(x => x.IsUsed);
            if (command.IsRemoved) query = query.Where(x => x.IsRemoved);
            if (command.IsExpired) query = query.Where(x => x.IsExpired);
            if (!string.IsNullOrWhiteSpace(command.PhoneNumber)) query = query.Where(x => x.PhoneNumber.Contains(command.PhoneNumber));

            var list = query.OrderByDescending(x => x.Id).ToList();
            list.ForEach(each => each.AccountName = context.Accounts.FirstOrDefault(x => x.PhoneNumber == each.PhoneNumber)?.Fullname ?? "?");

            return list;
        }

        public string GetPhoneNumberByHash(string hash) => context.ForgetPasswordLogs.FirstOrDefault(x => x.Hash == hash)?.PhoneNumber ?? null;
    }
}
