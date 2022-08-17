using AccountSection.Application.Contracts.AccountApp;
using AccountSection.Domain.AccountAgg;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountSection.Infrastructure.EFCore.Repositories
{
    public class AccountRepo : BaseEfRepo<SaveAccount, SearchAccount, ViewAccount, Account>, IAccountRepo
    {
        #region Init
        private readonly AccountContext context;

        public AccountRepo(AccountContext context) : base(context) => this.context = context;
        #endregion

        public SaveAccount GetByPhoneNumber(string phoneNumber) =>
            context.Accounts.Select(x => new SaveAccount
            {
                Id = x.Id,
                Email = x.Email,
                RoleId = x.RoleId,
                Fullname = x.Fullname,
                Password = x.Password,
                IsRemoved = x.IsRemoved,
                PhoneNumber = x.PhoneNumber,
                VerificationCode = x.VerificationCode,
            }).AsNoTracking().FirstOrDefault(x => x.PhoneNumber.Contains(phoneNumber) && !x.IsRemoved);

        public override List<ViewAccount> Search(SearchAccount command)
        {
            var query = new ViewAccount().FromList(context.Accounts.AsNoTracking());

            if (command.IsRemoved) query = query.Where(x => x.IsRemoved);
            if (!string.IsNullOrWhiteSpace(command.Name)) query = query.Where(x => x.Name.Contains(command.Name));
            if (!string.IsNullOrWhiteSpace(command.Email)) query = query.Where(x => x.Email.Contains(command.Email));

            var list = query.OrderByDescending(x => x.Id).ToList();
            list.ForEach(each => each.Role = context.Roles.SingleOrDefault(x => x.Id == each.RoleId)?.Name ?? "?");

            return list;
        }
    }
}
