using Common.Domain;
using Common.Application.Contracts;

namespace Common.Application
{
    public class BaseJsonApplication<TRepo, TDomain> : IBaseJsonApplication<TDomain> where TRepo : IBaseJsonRepo<TDomain> where TDomain : new()
    {
        #region Init
        protected readonly TRepo repo;

        public BaseJsonApplication(TRepo repo) => this.repo = repo;
        #endregion

        public TDomain Get() => repo.Get();
        public void Set(TDomain command) => repo.Set(command);
    }
}
