using Common.Application;
using System.Threading.Tasks;
using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.AccountApp
{
    public interface IAccountApplication : IBaseEfApplication<SaveAccount, SearchAccount, ViewAccount>
    {
        TaskResult LogOut();
        TaskResult Ban(long id);
        TaskResult Unban(long id);
        TaskResult SetVerificationCodeToOk(long id);
        TaskResult ChangeRole(ChangeRoleDto command);
        Task<TaskResult> LogInAsync(LogInDto command);
        SaveAccount GetByPhoneNumber(string phoneNumber);
        TaskResult ChangePassword(ChangePasswordDto command);
    }
}
