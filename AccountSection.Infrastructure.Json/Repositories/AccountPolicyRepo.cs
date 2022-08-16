using Common.Infrastructure;
using AccountSection.Domain.AccountPolicyAgg;
using AccountSection.Application.Contracts.AccountPolicyApp;

namespace AccountSection.Infrastructure.Json.Repositories
{
    public class AccountPolicyRepo : BaseJsonRepo<AccountPolicy>, IAccountPolicyRepo
    {
    }
}
