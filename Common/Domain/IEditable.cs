namespace Common.Domain
{
    public interface IEditable<TDomain>
    {
        void Edit(TDomain edited);
    }
}
