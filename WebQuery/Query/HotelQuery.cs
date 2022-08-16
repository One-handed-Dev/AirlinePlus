using System;
using System.Linq;
using Common.Application;
using System.Collections.Generic;
using WebQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using WebQuery.Contracts.Hotel.Hotel;
using HotelSection.Domain.PictureAgg;
using HotelSection.Infrastructure.EFCore;
using WebQuery.Contracts.Hotel.HotelPicture;
using HotelSection.Application.Contracts.OrderApp;

namespace WebQuery.Query
{
    public class HotelQuery : IHotelQuery
    {
        #region Init
        private readonly HotelContext context;
        private readonly ICommentQuery commentQuery;

        public HotelQuery(HotelContext context, ICommentQuery commentQuery)
        {
            this.context = context;
            this.commentQuery = commentQuery;
        }
        #endregion

        public QueryHotel GetDetails(long id) 
        {
            var hotel = context.Hotels
                .AsNoTracking()
                .Include(x => x.Rooms)
                .Include(x => x.HotelPictures)
                .FirstOrDefault(x => x.Id == id);

            if (hotel is null) return default;

            var query = new QueryHotel().From(hotel);

            query.HotelPictures = MapPictures(hotel.HotelPictures);
            query.Comments = commentQuery.GetHotelComments(query.Id);
            query.CountOfRooms = query.Rooms.Sum(x => x.CountOfThisRoomTypeInHotel);

            return query;
        }

        public QueryHotel[] Search(string query) => new QueryHotel()
            .FromList(context.Hotels.AsNoTracking()
            .Where(x => x.Name.Contains(query) || x.City.Contains(query) || x.Address.Contains(query)))
            .ToArray();

        public List<CartItem> FetchData(List<CartItem> cartItems)
        {
            cartItems.ForEach(each =>
            {
                var room = context.Rooms.SingleOrDefault(x => x.Id == each.Id);

                if (room is not null)
                {
                    var hotel = context.Hotels.SingleOrDefault(x => x.Id == room.HotelId);

                    each.HotelId = hotel.Id;
                    each.Price = room.Price;
                    each.Picture = hotel.Picture;
                    each.Name = hotel.Name + " - " + room.Name;
                }
            });

            return cartItems;
        }

        public QueryHotel[] GetSameCityHotels(string city, long id) => new QueryHotel().FromList(context.Hotels.AsNoTracking()
            .Where(x => x.Id != id)
            .Where(x => x.City == city)
            ).ToArray();

        private static List<QueryHotelPicture> MapPictures(List<HotelPicture> pictures) 
            => pictures.Select(x => new QueryHotelPicture()
            {
                Id = x.Id,
                HotelId = x.HotelId,
                Picture = x.Picture,
                IsRemoved = x.IsRemoved,
            }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).ToList();

        public QueryHotel[] GetAll() => new QueryHotel().FromList(context.Hotels.AsNoTracking()).ToArray();
    }
}
