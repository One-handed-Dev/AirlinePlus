using System;
using LinqKit;
using Common.Domain;
using System.Linq.Expressions;
using System.Collections.Generic;
using Common.Application.Contracts;

namespace Common.Application
{
    public class BaseEfApplication<TRepo, TSave, TSearch, TView, TDomain> : IBaseEfFullApplication<TSave, TSearch, TView, TDomain>

        where TSave : BaseEfSaveModel
        where TView : BaseEfViewModel
        where TSearch : BaseEfSearchModel
        where TDomain : BaseEfDomainModel, new()
        where TRepo : IBaseEfRepo<TSave, TSearch, TView, TDomain>
    {
        #region Init
        protected readonly TRepo repo;

        public BaseEfApplication(TRepo repo) => this.repo = repo;
        #endregion

        public TaskResult Remove(long id)
        {
            TaskResult operation = new();
            var target = repo.Get(id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.Remove();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult Restore(long id)
        {
            TaskResult operation = new();
            var target = repo.Get(id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.Restore();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public List<TView> GetSelectList() => repo.GetSelectList();

        public virtual TSave GetDetails(long id) => repo.GetDetails(id);

        public List<TView> Search(TSearch command) => repo.Search(command);

        public virtual TaskResult Edit(TSave command) => throw new NotImplementedException();

        public virtual TaskResult Create(TSave command) => throw new NotImplementedException();

        public TaskResult Edit(TSave command, Expression<Func<TDomain, bool>> duplicationCheckExpression)
        {
            TaskResult operation = new();
            var target = repo.Get(command.Id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            var mixedExpression = PredicateBuilder.New(duplicationCheckExpression).And(x => x.Id != command.Id);

            if (duplicationCheckExpression != default && repo.Exists(mixedExpression))
                return operation.Failed(TaskResult.Messages.RecordIsDuplicated);

            var toEdit = new TDomain().From(command);

            target.Edit(toEdit);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult Create(TSave command, Expression<Func<TDomain, bool>> duplicationCheckExpression)
        {
            TaskResult operation = new();

            if (duplicationCheckExpression != default && repo.Exists(duplicationCheckExpression))
                return operation.Failed(TaskResult.Messages.RecordIsDuplicated);

            var @new = new TDomain().From(command);

            repo.Create(@new);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }
    }
}
