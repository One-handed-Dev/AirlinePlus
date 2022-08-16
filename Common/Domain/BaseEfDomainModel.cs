using System;
using Common.Application;

namespace Common.Domain
{
    public class BaseEfDomainModel : ICreation<DateTime>, IFullRemovable
    {
        #region Props
        public long Id { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion

        #region Methods
        public TaskResult Remove()
        {
            IsRemoved = true;
            return new TaskResult().Succedded();
        }

        public TaskResult Restore()
        {
            IsRemoved = false;
            return new TaskResult().Succedded();
        }

        public BaseEfDomainModel(long id) => Id = id;

        public BaseEfDomainModel() => CreationDate = DateTime.Now;

        public virtual void Edit<T>(T edited) where T : BaseEfDomainModel => throw new NotImplementedException();
        #endregion
    }
}
