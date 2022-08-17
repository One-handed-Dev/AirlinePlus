using Common.Application;
using Common.Application.Contracts;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Infrastructure
{
    public class BaseEfRepo<TSave, TSearch, TView, TDomain> : IBaseEfRepo<TSave, TSearch, TView, TDomain>

        where TSave : BaseEfSaveModel
        where TView : BaseEfViewModel
        where TSearch : BaseEfSearchModel
        where TDomain : BaseEfDomainModel
    {
        #region Init
        private readonly DbContext context;

        public BaseEfRepo(DbContext context) => this.context = context;
        #endregion

        public virtual TSave GetDetails(long id) =>
            ((TSave)Activator.CreateInstance(typeof(TSave), Array.Empty<object>()))
            .From(context.Set<TDomain>().Find(id), Projection.CalendarMode.ToJalali);

        public virtual List<TView> GetSelectList() =>
            ((TView)Activator.CreateInstance(typeof(TView), Array.Empty<object>()))
            .GetSelectList(context.Set<TDomain>().AsNoTracking(), Projection.SelectListType.JustNonRemoved);

        public bool Save() => context.SaveChanges() > 0;

        public void Create(TDomain entity) => context.Add(entity);

        public virtual TDomain Get(long id) => context.Find<TDomain>(id);

        public List<TDomain> GetAll() => context.Set<TDomain>().AsNoTracking().ToList();

        public virtual List<TView> Search(TSearch command) => throw new NotImplementedException();

        public bool Exists(Expression<Func<TDomain, bool>> expression) => context.Set<TDomain>().Any(expression);
    }
}
