namespace Common.Application.Contracts
{
    public class ViewSimple : BaseEfViewModel
    {
        public ViewSimple() { }

        public ViewSimple(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
