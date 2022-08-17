using Common.Domain;
using InteractionSection.Application.Contracts.EmailApp;
using InteractionSection.Application.EmailApp;
using InteractionSection.Domain.EmailAgg;
using InteractionSection.Infrastructure.Config.Permissions;
using InteractionSection.Infrastructure.EFCore;
using InteractionSection.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
