using AccountSection.Application.Contracts.ForgetPasswordLogApp;
using Common.Application;
using Common.Domain;
using System;
using System.Linq.Expressions;

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
