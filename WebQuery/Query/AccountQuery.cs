using System.Linq;
using WebQuery.Contracts.Account;
using AccountSection.Domain.AccountAgg;
using AccountSection.Infrastructure.EFCore;
using AccountSection.Application.Contracts.AccountApp;

namespace WebQuery.Query
{
    public sealed class AccountQuery : IAccountQuery
    {
        #region Init
        private readonly AccountContext context;

        public AccountQuery(AccountContext context) => this.context = context;
        #endregion

        public Account Get(long id)
        {
            var account = context.Accounts.FirstOrDefault(x => x.Id == id);
            return account;
        }

        public void Edit(SaveAccount command)
        {
            var target = context.Accounts.FirstOrDefault(x => x.Id == command.Id);

            if (target is null) return;

            target.Email = command.Email;
            target.Fullname = command.Fullname;
            target.PhoneNumber = command.PhoneNumber;

            context.SaveChanges();
        }
    }
}
