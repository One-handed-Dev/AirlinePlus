using Common.Domain;
using AccountSection.Application.Contracts.AccountPolicyApp;

namespace AccountSection.Domain.AccountPolicyAgg
{
    public interface IAccountPolicyRepo : IBaseJsonRepo<AccountPolicy>
    {
    }
}
