using System.Linq;
using Common.Application;
using System.Collections.Generic;
using WebQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using ShopSection.Infrastructure.EFCore;
using ShopSection.Application.Contracts.OrderApp;
using WebQuery.Contracts.Shop.Airline;

namespace WebQuery.Query
{
    public class AirlineQuery : IAirlineQuery
    {
        #region Init
        private readonly ShopContext context;
        private readonly ICommentQuery commentQuery;

        public AirlineQuery(ShopContext context, ICommentQuery commentQuery)
        {
            this.context = context;
            this.commentQuery = commentQuery;
        }
        #endregion

        public QueryAirline GetDetails(long id)
        {
            var shop = context.Airlines
                .AsNoTracking()
                .Include(x => x.Flights)
                .FirstOrDefault(x => x.Id == id);

            if (shop is null) return default;

            var query = new QueryAirline().From(shop);

            query.Comments = commentQuery.GetShopComments(query.Id);
            //query.CountOfFlights = query.Flights.Sum(x => x.CountOfThisFlightTypeInShop);

            return query;
        }

        public QueryAirline[] Search(string query) => new QueryAirline()
            .FromList(context.Flights.Include(x => x.Airline).AsNoTracking()
            .Where(x => x.Airline.Name.Contains(query) || x.SourceCity.Contains(query) || x.DestinationCity.Contains(query)))
            .ToArray();

        public List<CartItem> FetchData(List<CartItem> cartItems)
        {
            cartItems.ForEach(each =>
            {
                var flight = context.Flights.SingleOrDefault(x => x.Id == each.Id);

                if (flight is not null)
                {
                    var airline = context.Airlines.SingleOrDefault(x => x.Id == flight.AirlineId);

                    each.ShopId = airline.Id;
                    each.Price = flight.Price;
                    each.Picture = airline.Picture;
                    each.Name = airline.Name + " - " + flight.SourceCity;
                }
            });

            return cartItems;
        }

        public QueryAirline[] GetAll() => new QueryAirline().FromList(context.Airlines.AsNoTracking()).ToArray();
    }
}
