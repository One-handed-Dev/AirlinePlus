using AccountSection.Application.Contracts.AccountPolicyApp;
using Common.Domain;

namespace AccountSection.Domain.AccountPolicyAgg
{
    public interface IAccountPolicyRepo : IBaseJsonRepo<AccountPolicy>
    {
    }
}
