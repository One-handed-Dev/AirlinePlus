using Common.Application;

namespace Common.Domain
{
    public class BaseJsonDomainModel<TDomain> : IEditable<TDomain> where TDomain : BaseJsonDomainModel<TDomain>
    {
        public long Id { get; set; }

        public void Edit(TDomain edited) => this.From(edited, Projection.CalendarMode.ToGregorian, Projection.NullOrEmptyPicture.Ignore);
    }
}
