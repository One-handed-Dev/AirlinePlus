using Common.Application;
using Common.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Classes
{
    public sealed class HostConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}
