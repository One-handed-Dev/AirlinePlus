using Common.Domain;
using System.Collections.Generic;

namespace Common.Application.Contracts
{
    public interface IBaseEfApplication<TSave, TSearch, TView> : IRemovableBy

        where TSave : BaseEfSaveModel
        where TView : BaseEfViewModel
        where TSearch : BaseEfSearchModel
    {
        TSave GetDetails(long id);
        List<TView> GetSelectList();
        TaskResult Edit(TSave command);
        TaskResult Create(TSave command);
        List<TView> Search(TSearch command);
    }
}
