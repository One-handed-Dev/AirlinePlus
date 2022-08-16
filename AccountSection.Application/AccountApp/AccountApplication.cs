using Common.Application;
using System.Threading.Tasks;
using Common.Application.Contracts;
using AccountSection.Domain.RoleAgg;
using AccountSection.Domain.AccountAgg;
using AccountSection.Application.Contracts.AccountApp;
using InteractionSection.Application.Contracts.EmailApp;

namespace AccountSection.Application.AccountApp
{
    public class AccountApplication : BaseEfApplication<IAccountRepo, SaveAccount, SearchAccount, ViewAccount, Account>, IAccountApplication
    {
        #region Init
        private readonly IRoleRepo roleRepo;
        private readonly IPasswordService passwordService;
        private readonly IEmailApplication emailApplication;
        private readonly IAuthenticationService authenticationService;

        public AccountApplication(IAccountRepo repo, IPasswordService passwordService, IAuthenticationService authenticationService, IRoleRepo roleRepo, IEmailApplication emailApplication) : base(repo)
        {
            this.roleRepo = roleRepo;
            this.passwordService = passwordService;
            this.emailApplication = emailApplication;
            this.authenticationService = authenticationService;
        }
        #endregion

        public TaskResult LogOut()
        {
            authenticationService.RemoveCookies();
            return new TaskResult().Succedded();
        }

        public TaskResult Ban(long id)
        {
            TaskResult operation = new();
            var target = repo.Get(id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.Ban();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult Unban(long id)
        {
            TaskResult operation = new();
            var target = repo.Get(id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.Unban();
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult SetVerificationCodeToOk(long id)
        {
            TaskResult operation = new();
            var target = repo.GetDetails(id);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.VerificationCode = "Ok";
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public TaskResult ChangeRole(ChangeRoleDto command)
        {
            TaskResult operation = new();
            var target = repo.Get(command.AccountId);

            if (target is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            target.ChangeRole(command.NewRoleId, command.ModifierAccountId);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public override TaskResult Create(SaveAccount command)
        {
            command.Password = passwordService.Hash(command.Password);
            return Create(command, x => x.PhoneNumber == command.PhoneNumber);
        }

        public async Task<TaskResult> LogInAsync(LogInDto command)
        {
            TaskResult operation = new();

            var account = repo.GetByPhoneNumber(command.PhoneNumber);

            if (account is null)
                return operation.Failed(TaskResult.Messages.WrongUsernameOrPassword);

            var (Verified, _) = passwordService.Check(new(account.Password, command.Password));
            if (!Verified)
                return operation.Failed(TaskResult.Messages.WrongUsernameOrPassword);

            var permissions = roleRepo.GetPermissionsByRoleId(account.RoleId);

            var authenticationModel = new AuthenticationDto(account.Id, account.RoleId, account.Fullname, account.PhoneNumber, permissions);

            var result = await authenticationService.PlaceCookiesAsync(authenticationModel);

            if (result) emailApplication.SendLogInNotif(account.Id);

            return operation.ReturnOkOnlyIfCommitted(result);
        }

        public TaskResult ChangePassword(ChangePasswordDto command)
        {
            TaskResult operation = new();
            var account = repo.Get(command.AccountId);

            if (account is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(TaskResult.Messages.PasswordNotMatched);

            var hashed = passwordService.Hash(command.Password);
            account.ChangePassword(hashed);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public SaveAccount GetByPhoneNumber(string phoneNumber) => repo.GetByPhoneNumber(phoneNumber);

        public override TaskResult Edit(SaveAccount command) => Edit(command, x => x.PhoneNumber == command.PhoneNumber);
    }
}
