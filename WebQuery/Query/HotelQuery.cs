using System;
using System.Linq;
using Common.Application;
using System.Collections.Generic;
using WebQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using WebQuery.Contracts.Shop.Shop;
using ShopSection.Domain.PictureAgg;
using ShopSection.Infrastructure.EFCore;
using WebQuery.Contracts.Shop.ShopPicture;
using ShopSection.Application.Contracts.OrderApp;

namespace WebQuery.Query
{
    public class ShopQuery : IShopQuery
    {
        #region Init
        private readonly ShopContext context;
        private readonly ICommentQuery commentQuery;

        public ShopQuery(ShopContext context, ICommentQuery commentQuery)
        {
            this.context = context;
            this.commentQuery = commentQuery;
        }
        #endregion

        public QueryShop GetDetails(long id) 
        {
            var hotel = context.Shops
                .AsNoTracking()
                .Include(x => x.Rooms)
                .Include(x => x.ShopPictures)
                .FirstOrDefault(x => x.Id == id);

            if (hotel is null) return default;

            var query = new QueryShop().From(hotel);

            query.ShopPictures = MapPictures(hotel.ShopPictures);
            query.Comments = commentQuery.GetShopComments(query.Id);
            query.CountOfRooms = query.Rooms.Sum(x => x.CountOfThisRoomTypeInShop);

            return query;
        }

        public QueryShop[] Search(string query) => new QueryShop()
            .FromList(context.Shops.AsNoTracking()
            .Where(x => x.Name.Contains(query) || x.City.Contains(query) || x.Address.Contains(query)))
            .ToArray();

        public List<CartItem> FetchData(List<CartItem> cartItems)
        {
            cartItems.ForEach(each =>
            {
                var room = context.Rooms.SingleOrDefault(x => x.Id == each.Id);

                if (room is not null)
                {
                    var hotel = context.Shops.SingleOrDefault(x => x.Id == room.ShopId);

                    each.ShopId = hotel.Id;
                    each.Price = room.Price;
                    each.Picture = hotel.Picture;
                    each.Name = hotel.Name + " - " + room.Name;
                }
            });

            return cartItems;
        }

        public QueryShop[] GetSameCityShops(string city, long id) => new QueryShop().FromList(context.Shops.AsNoTracking()
            .Where(x => x.Id != id)
            .Where(x => x.City == city)
            ).ToArray();

        private static List<QueryShopPicture> MapPictures(List<ShopPicture> pictures) 
            => pictures.Select(x => new QueryShopPicture()
            {
                Id = x.Id,
                ShopId = x.ShopId,
                Picture = x.Picture,
                IsRemoved = x.IsRemoved,
            }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).ToList();

        public QueryShop[] GetAll() => new QueryShop().FromList(context.Shops.AsNoTracking()).ToArray();
    }
}
