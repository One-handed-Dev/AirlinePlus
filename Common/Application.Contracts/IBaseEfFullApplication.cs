using System;
using System.Linq.Expressions;

namespace Common.Application.Contracts
{
    interface IBaseEfFullApplication<TSave, TSearch, TView, TDomain> : IBaseEfApplication<TSave, TSearch, TView>

        where TSave : BaseEfSaveModel
        where TView : BaseEfViewModel
        where TSearch : BaseEfSearchModel
    {
        TaskResult Edit(TSave command, Expression<Func<TDomain, bool>> duplicationCheckExpression);
        TaskResult Create(TSave command, Expression<Func<TDomain, bool>> duplicationCheckExpression);
    }
}
