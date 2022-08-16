using Common.Domain;

namespace Common.Application.Contracts
{
    public class BaseEfViewModel : ICreation<string>, IRemovable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
    }
}
