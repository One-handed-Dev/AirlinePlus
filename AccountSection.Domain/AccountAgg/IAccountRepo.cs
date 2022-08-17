using AccountSection.Application.Contracts.AccountApp;
using Common.Domain;

namespace AccountSection.Domain.AccountAgg
{
    public interface IAccountRepo : IBaseEfRepo<SaveAccount, SearchAccount, ViewAccount, Account>
    {
        SaveAccount GetByPhoneNumber(string phoneNumber);
    }
}