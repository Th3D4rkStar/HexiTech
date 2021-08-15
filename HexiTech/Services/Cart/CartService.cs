
using HexiTech.Data.Models;

namespace HexiTech.Services.Cart
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;

    public class CartService : ICartService
    {
        private readonly HexiTechDbContext db;
        private readonly IMapper mapper;

        public CartService(HexiTechDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool AddToCart(string userId, int productId)
        {
            if (db.UserShoppingCarts.Any(usc => usc.UserId == userId && usc.ProductId == productId))
            {
                db.UserShoppingCarts
                    .FirstOrDefault(usc =>
                        usc.UserId == userId && usc.ProductId == productId)
                        .Quantity++;

                db.SaveChanges();
            }
            else
            {
                db.UserShoppingCarts.Add(new UserShoppingCart()
                {
                    UserId = userId,
                    ProductId = productId
                });

                db.SaveChanges();
            }


            return true;
        }

        public bool Remove(string userId, int cartItemId)
        {
            throw new System.NotImplementedException();
        }

        public int Count(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CartItemServiceModel> GetCartItemsByUser(string userId)
            => db.UserShoppingCarts
                .Where(usc => usc.UserId == userId)
                .OrderByDescending(usc => usc.DateAdded)
                .ProjectTo<CartItemServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
    }
}