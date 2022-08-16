using Common.Application;

namespace Common.Domain
{
    public interface IFullRemovable : IRemovable
    {
        public TaskResult Remove();
        public TaskResult Restore();
    }
}
