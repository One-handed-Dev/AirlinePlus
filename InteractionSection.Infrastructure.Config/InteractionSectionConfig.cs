using Common.Domain;
using Microsoft.EntityFrameworkCore;
using InteractionSection.Domain.EmailAgg;
using InteractionSection.Application.EmailApp;
using InteractionSection.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using InteractionSection.Domain.DashboardStatisticAgg;
using InteractionSection.Application.Contracts.EmailApp;
using InteractionSection.Application.DashboardStatisticApp;
using InteractionSection.Infrastructure.Config.Permissions;
using InteractionSection.Infrastructure.EFCore.Repositories;
using InteractionSection.Application.Contracts.DashboardStatisticApp;

namespace InteractionSection.Infrastructure.Config
{
    public static class InteractionSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<InteractionContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<IPermissionExposer, InteractionPermissionExposer>();

            services.AddTransient<IEmailRepo, EmailRepo>(); services.AddTransient<IEmailApplication, EmailApplication>();
            services.AddTransient<IDashboardStatisticRepo, DashboardStatisticRepo>(); services.AddTransient<IDashboardStatisticApplication, DashboardStatisticApplication>();
        }
    }
}
