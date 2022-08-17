using CommentSection.Application.CommentApp;
using CommentSection.Application.Contracts.CommentApp;
using CommentSection.Domain.CommentAgg;
using CommentSection.Infrastructure.Config.Permissions;
using CommentSection.Infrastructure.EFCore;
using CommentSection.Infrastructure.EFCore.Repositories;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebQuery.Contracts.Comment;
using WebQuery.Query;

namespace CommentSection.Infrastructure.Config
{
    public sealed class CommentSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<ICommentQuery, CommentQuery>();
            services.AddTransient<IPermissionExposer, CommentPermissionExposer>();

            services.AddTransient<ICommentRepo, CommentRepo>(); services.AddTransient<ICommentApplication, CommentApplication>();
        }
    }
}
