using Common.Domain;
using Microsoft.EntityFrameworkCore;
using InteractionSection.Domain.EmailAgg;
using InteractionSection.Application.EmailApp;
using Microsoft.Extensions.DependencyInjection;
using InteractionSection.Infrastructure.EFCore;
using InteractionSection.Application.Contracts.EmailApp;
using InteractionSection.Infrastructure.Config.Permissions;
using InteractionSection.Infrastructure.EFCore.Repositories;

namespace InteractionSection.Infrastructure.Config
{
    public static class InteractionSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<InteractionContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<IPermissionExposer, InteractionPermissionExposer>();

            services.AddTransient<IEmailRepo, EmailRepo>(); services.AddTransient<IEmailApplication, EmailApplication>();
        }
    }
}
