using Common.Application;
using Common.Infrastructure;
using InteractionSection.Application.Contracts.EmailApp;
using InteractionSection.Domain.EmailAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
