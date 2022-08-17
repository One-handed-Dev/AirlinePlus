using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopSection.Domain.AirlineAgg;
using ShopSection.Application.Contracts.AirlineApp;

namespace ShopSection.Infrastructure.EFCore.Repositories
{
    public class AirlineRepo : BaseEfRepo<SaveAirline, SearchAirline, ViewAirline, Airline>, IAirlineRepo
    {
        #region Init
        private readonly ShopContext context;

        public AirlineRepo(ShopContext context) : base(context) => this.context = context;
        #endregion

        public override List<ViewAirline> Search(SearchAirline command)
        {
            var query = new ViewAirline().FromList(context.Airlines.AsNoTracking());

            if (command.IsRemoved) query = query.Where(x => x.IsRemoved);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
