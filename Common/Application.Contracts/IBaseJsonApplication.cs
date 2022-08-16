namespace Common.Application.Contracts
{
    public interface IBaseJsonApplication<TDomain>
    {
        TDomain Get();
        void Set(TDomain command);
    }
}
