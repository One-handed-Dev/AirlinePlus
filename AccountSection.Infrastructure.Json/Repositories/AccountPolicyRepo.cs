using AccountSection.Application.Contracts.AccountPolicyApp;
using AccountSection.Domain.AccountPolicyAgg;
using Common.Infrastructure;

namespace AccountSection.Infrastructure.Json.Repositories
{
    public class AccountPolicyRepo : BaseJsonRepo<AccountPolicy>, IAccountPolicyRepo
    {
    }
}
