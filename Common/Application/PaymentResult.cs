namespace Common.Application
{
    public sealed record PaymentResult
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public string IssueTrackingNo { get; set; }

        public PaymentResult Failed(string message = TaskResult.Messages.IsFailed)
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }

        public PaymentResult Succeeded(string issueTrackingNo, string message = TaskResult.Messages.IsSuccessful)
        {
            IssueTrackingNo = issueTrackingNo;
            IsSuccessful = true;
            Message = message;
            return this;
        }
    }
}
