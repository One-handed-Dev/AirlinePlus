using AccountSection.Application.Contracts.AccountPolicyApp;
using AccountSection.Domain.AccountPolicyAgg;
using Common.Application;

namespace AccountSection.Application.AccountPolicyApp
{
    public class AccountPolicyApplication : BaseJsonApplication<IAccountPolicyRepo, AccountPolicy>, IAccountPolicyApplication
    {
        #region Init
        public AccountPolicyApplication(IAccountPolicyRepo repo) : base(repo) { }
        #endregion
    }
}
