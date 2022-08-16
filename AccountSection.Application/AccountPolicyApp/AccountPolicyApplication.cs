using Common.Application;
using AccountSection.Domain.AccountPolicyAgg;
using AccountSection.Application.Contracts.AccountPolicyApp;

namespace AccountSection.Application.AccountPolicyApp
{
    public class AccountPolicyApplication : BaseJsonApplication<IAccountPolicyRepo, AccountPolicy>, IAccountPolicyApplication
    {
        #region Init
        public AccountPolicyApplication(IAccountPolicyRepo repo) : base(repo) { }
        #endregion
    }
}
