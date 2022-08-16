using System;
using Common.Domain;
using Common.Application;
using System.Linq.Expressions;
using AccountSection.Application.Contracts.ForgetPasswordLogApp;

namespace AccountSection.Domain.ForgetPasswordLogAgg
{
    public interface IForgetPasswordLogRepo : IBaseEfRepo<SaveForgetPasswordLog, SearchForgetPasswordLog, ViewForgetPasswordLog, ForgetPasswordLog>
    {
        TaskResult Use(string hash);
        TaskResult UpdateExpirationStatus();
        string GetPhoneNumberByHash(string hash);
        string GetValidHash(Expression<Func<ForgetPasswordLog, bool>> expression);
    }
}
