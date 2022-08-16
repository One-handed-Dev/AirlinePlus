namespace Common.Application
{
    public sealed class TaskResult
    {
        #region Init
        public sealed class Messages
        {
            public const string IsFailed = "عملیات با خطا مواجه شد";
            public const string IsSuccessful = "عملیات با موفقیت انجام شد";
            public const string PasswordNotMatched = "رمز و تکرار رمز مطابقت ندارند";
            public const string RecordNotFound = "رکورد با اطلاعات درخواست شده یافت نشد";
            public const string RecordIsDuplicated = "امکان ثبت رکورد تکراری وجود ندارد";
            public const string WrongUsernameOrPassword = "نام کاربری یا رمزعبور اشتباه است";
            public const string NotEnoughInformation = "اطلاعات موردنیاز برای انجام عملیات، کافی نیست";
            public const string ResetPasswordExpired = "مدتی از زمان آخرین درخواست بازیابی‌تان گذشته‌است. لطفا مجددا درخواست فراموشی رمزعبور ثبت‌کنید.";
        }

        public string Message { get; private set; }
        public bool IsSuccedded { get; private set; }

        public TaskResult() => IsSuccedded = false;
        #endregion

        public TaskResult ReturnOkOnlyIfCommitted(bool isCommitted)
        {
            if (isCommitted) return SetIsSucceddedAndReturnInstance(true, Messages.IsSuccessful);
            else return SetIsSucceddedAndReturnInstance(false, Messages.IsFailed);
        }

        private TaskResult SetIsSucceddedAndReturnInstance(bool situation, string message)
        {
            IsSuccedded = situation;
            Message = message;
            return this;
        }

        public TaskResult Failed(string message = Messages.IsFailed) => SetIsSucceddedAndReturnInstance(false, message);

        public TaskResult Succedded(string message = Messages.IsSuccessful) => SetIsSucceddedAndReturnInstance(true, message);
    }
}
