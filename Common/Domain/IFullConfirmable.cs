namespace Common.Domain
{
    public interface IFullConfirmable : IConfirmable
    {
        public void Cancel();
        public void Confirm();
    }
}
