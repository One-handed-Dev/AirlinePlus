namespace Common.Domain
{
    public interface IConfirmable
    {
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
