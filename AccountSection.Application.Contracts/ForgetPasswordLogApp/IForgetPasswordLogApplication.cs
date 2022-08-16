using Common.Application;
using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.ForgetPasswordLogApp
{
    public interface IForgetPasswordLogApplication : IBaseEfApplication<SaveForgetPasswordLog, SearchForgetPasswordLog, ViewForgetPasswordLog>
    {
        TaskResult RegisterRequest(string phoneNumber);
        TaskResult RecoverPassword(RecoverPasswordDto command);
    }
}
