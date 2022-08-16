using AccountSection.Application.Contracts.AccountApp;

namespace WebQuery.Contracts.Account
{
    public interface IAccountQuery
    {
        void Edit(SaveAccount command);
        AccountSection.Domain.AccountAgg.Account Get(long id);
    }
}
