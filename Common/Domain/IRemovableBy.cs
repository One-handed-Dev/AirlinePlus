using Common.Application;

namespace Common.Domain
{
    public interface IRemovableBy
    {
        public TaskResult Remove(long id);
        public TaskResult Restore(long id);
    }
}
