using Common.Domain;
using AccountSection.Application.Contracts.AccountApp;

namespace AccountSection.Domain.AccountAgg
{
    public interface IAccountRepo : IBaseEfRepo<SaveAccount, SearchAccount, ViewAccount, Account>
    {
        SaveAccount GetByPhoneNumber(string phoneNumber);
    }
}