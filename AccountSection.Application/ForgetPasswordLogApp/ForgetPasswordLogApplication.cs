using AccountSection.Application.Contracts.AccountApp;
using AccountSection.Application.Contracts.ForgetPasswordLogApp;
using AccountSection.Domain.ForgetPasswordLogAgg;
using Common.Application;

namespace AccountSection.Application.ForgetPasswordLogApp
{
    public class ForgetPasswordLogApplication : BaseEfApplication<IForgetPasswordLogRepo, SaveForgetPasswordLog, SearchForgetPasswordLog, ViewForgetPasswordLog, ForgetPasswordLog>, IForgetPasswordLogApplication
    {
        #region Init
        private readonly IAccountApplication accountApplication;

        public ForgetPasswordLogApplication(IForgetPasswordLogRepo repo, IAccountApplication accountApplication) : base(repo)
        {
            this.accountApplication = accountApplication;
        }
        #endregion

        public TaskResult RegisterRequest(string phoneNumber)
        {
            TaskResult task = new();
            var account = accountApplication.GetByPhoneNumber(phoneNumber);

            if (account is null)
                return task.Failed(TaskResult.Messages.RecordNotFound);

            var record = new SaveForgetPasswordLog()
            {
                PhoneNumber = phoneNumber,
                Hash = CodeGenerator.GenerateRandomString(15),
            };

            var result = Create(record);
            return result;
        }

        public TaskResult RecoverPassword(RecoverPasswordDto command)
        {
            TaskResult task = new();
            var storedHash = repo.GetValidHash(x => x.Hash == command.Hash);

            if (storedHash is null)
                return task.Failed(TaskResult.Messages.ResetPasswordExpired);

            var phoneNumber = repo.GetPhoneNumberByHash(storedHash);
            var account = accountApplication.GetByPhoneNumber(phoneNumber);

            if (account is null)
                return task.Failed(TaskResult.Messages.RecordNotFound);

            var result1 = repo.Use(storedHash);
            if (!result1.IsSuccedded) return result1;

            var result2 = accountApplication.ChangePassword(new(account.Id, command.NewPassword, command.NewPassword));
            return result2;
        }

        public override TaskResult Edit(SaveForgetPasswordLog command) => Edit(command, x => x.Hash == command.Hash);

        public override TaskResult Create(SaveForgetPasswordLog command) => Create(command, x => x.Hash == command.Hash);
    }
}
