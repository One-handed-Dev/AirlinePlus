using Common.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Domain
{
    public interface IBaseEfRepo<TSave, TSearch, TView, TDomain>

        where TSave : BaseEfSaveModel
        where TView : BaseEfViewModel
        where TSearch : BaseEfSearchModel
        where TDomain : BaseEfDomainModel
    {
        bool Save();
        TDomain Get(long id);
        List<TDomain> GetAll();
        TSave GetDetails(long id);
        void Create(TDomain entity);
        List<TView> GetSelectList();
        List<TView> Search(TSearch command);
        bool Exists(Expression<Func<TDomain, bool>> expression);
    }
}