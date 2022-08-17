using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using ShopSection.Domain.FlightAgg;
using Microsoft.EntityFrameworkCore;
using ShopSection.Application.Contracts.Common;
using ShopSection.Application.Contracts.FlightApp;

namespace ShopSection.Infrastructure.EFCore.Repositories
{
    public class FlightRepo : BaseEfRepo<SaveFlight, SearchFlight, ViewFlight, Flight>, IFlightRepo
    {
        #region Init
        private readonly ShopContext context;

        public FlightRepo(ShopContext context) : base(context) => this.context = context;
        #endregion

        public override List<ViewFlight> Search(SearchFlight command)
        {
            var query = context.Flights
                .AsNoTracking()
                .Include(x => x.Airline)
                .Select(x => new ViewFlight()
                {
                    Id = x.Id,
                    Day = x.Day,
                    Price = x.Price,
                    Capacity = x.Capacity,
                    IsRemoved = x.IsRemoved,
                    AirlineId = x.AirlineId,
                    StartHour = x.StartHour,
                    SourceCity = x.SourceCity,
                    AirlineString = x.Airline.Name,
                    DestinationCity = x.DestinationCity,
                    DayString = x.Day.ToFriendlyString(),
                    CreationDate = x.CreationDate.ToJalaliString(),
                    TimeOfFlightDurationHours = x.TimeOfFlightDurationHours,
                });

            if (command.IsRemoved) query = query.Where(x => x.IsRemoved);
            if (command.Day > 0) query = query.Where(x => x.Day == command.Day);
            if (command.AirlineId > 0) query = query.Where(x => x.AirlineId == command.AirlineId);
            if (!string.IsNullOrWhiteSpace(command.SourceCity)) query = query.Where(x => x.SourceCity.Contains(command.SourceCity));
            if (!string.IsNullOrWhiteSpace(command.DestinationCity)) query = query.Where(x => x.DestinationCity.Contains(command.DestinationCity));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
