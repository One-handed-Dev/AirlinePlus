using AccountSection.Application.AccountApp;
using AccountSection.Application.AccountPolicyApp;
using AccountSection.Application.Contracts.AccountApp;
using AccountSection.Application.Contracts.AccountPolicyApp;
using AccountSection.Application.Contracts.ForgetPasswordLogApp;
using AccountSection.Application.Contracts.RoleApp;
using AccountSection.Application.ForgetPasswordLogApp;
using AccountSection.Application.RoleApp;
using AccountSection.Domain.AccountAgg;
using AccountSection.Domain.AccountPolicyAgg;
using AccountSection.Domain.ForgetPasswordLogAgg;
using AccountSection.Domain.RoleAgg;
using AccountSection.Infrastructure.Config.Permissions;
using AccountSection.Infrastructure.EFCore;
using AccountSection.Infrastructure.EFCore.Repositories;
using AccountSection.Infrastructure.Json.Repositories;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebQuery.Contracts.Account;
using WebQuery.Query;

namespace AccountSection.Infrastructure.Config
{
    public class AccountSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<IAccountQuery, AccountQuery>();
            services.AddTransient<IPermissionExposer, AccountPermissionExposer>();

            services.AddTransient<IRoleRepo, RoleRepo>(); services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IAccountRepo, AccountRepo>(); services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountPolicyRepo, AccountPolicyRepo>(); services.AddTransient<IAccountPolicyApplication, AccountPolicyApplication>();
            services.AddTransient<IForgetPasswordLogRepo, ForgetPasswordLogRepo>(); services.AddTransient<IForgetPasswordLogApplication, ForgetPasswordLogApplication>();
        }
    }
}

//  dependency injection (DI): تزریق وابستگی