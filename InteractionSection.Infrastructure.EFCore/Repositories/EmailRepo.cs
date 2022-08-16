using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using InteractionSection.Domain.EmailAgg;
using InteractionSection.Application.Contracts.EmailApp;

namespace InteractionSection.Infrastructure.EFCore.Repositories
{
    public class EmailRepo : BaseEfRepo<SaveEmail, SearchEmail, ViewEmail, Email>, IEmailRepo
    {
        #region Init
        private readonly InteractionContext context;

        public EmailRepo(InteractionContext context) : base(context) => this.context = context;
        #endregion

        public override List<ViewEmail> Search(SearchEmail command)
        {
            var query = new ViewEmail().FromList(context.Emails.AsNoTracking(), Projection.DateTimeMode.BothDateAndTime);

            if (!string.IsNullOrWhiteSpace(command.Subject)) query = query.Where(x => x.Subject.Contains(command.Subject));
            if (!string.IsNullOrWhiteSpace(command.RecieverName)) query = query.Where(x => x.RecieverName.Contains(command.RecieverName));
            if (!string.IsNullOrWhiteSpace(command.RecieverEmail)) query = query.Where(x => x.RecieverEmail.Contains(command.RecieverEmail));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
