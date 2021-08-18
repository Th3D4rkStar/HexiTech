namespace HexiTech.Services.Cart
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using HexiTech.Data.Models;

    public class CartService : ICartService
    {
        private readonly HexiTechDbContext db;
        private readonly IConfigurationProvider mapper;

        public CartService(HexiTechDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        }

        public bool AddToCart(string userId, int productId, int quantity)
        {
            if (db.UserShoppingCarts.Any(usc => usc.UserId == userId && usc.ProductId == productId))
            {
                var cartItem = db.UserShoppingCarts.Include(usc => usc.Product)
                    .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId);

                if (cartItem.Quantity + quantity > cartItem.Product.Quantity)
                {
                    cartItem.Quantity = cartItem.Product.Quantity;
                    db.SaveChanges();

                    return true;
                }

                db.UserShoppingCarts
                    .FirstOrDefault(usc =>
                        usc.UserId == userId && usc.ProductId == productId)
                        .Quantity += quantity;

                db.SaveChanges();
            }
            else
            {
                var product = db.Products.FirstOrDefault(p => p.Id == productId);

                if (quantity > product.Quantity)
                {
                    quantity = product.Quantity;
                    db.SaveChanges();
                }

                db.UserShoppingCarts.Add(new UserShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                });

                db.SaveChanges();
            }

            return true;
        }

        public bool Remove(string userId, int productId)
        {
            var userCart = db.UserShoppingCarts
                    .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId);

            if (userCart == null)
            {
                return false;
            }

            db.UserShoppingCarts.Remove(userCart);

            db.SaveChanges();

            return true;
        }

        public IEnumerable<CartItemServiceModel> GetCartItemsByUser(string userId)
            => db.UserShoppingCarts
                .Where(usc => usc.UserId == userId)
                .OrderByDescending(usc => usc.DateAdded)
                .Include(usc=>usc.Product)
                .ProjectTo<CartItemServiceModel>(this.mapper)
                .ToList();

        public int Count(string userId)
        {
            int itemsCount = db.UserShoppingCarts.Count(usc => usc.UserId == userId);

            return itemsCount;
        }

        public bool IncreaseQuantity(string userId, int productId)
        {
            if (db.UserShoppingCarts
                .Any(usc => usc.UserId == userId && usc.ProductId == productId))
            {
                if (db.UserShoppingCarts
                        .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId).Quantity
                    < db.Products
                        .FirstOrDefault(p => p.Id == productId).Quantity
                    && db.UserShoppingCarts
                        .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId).Quantity
                    < 100)
                {
                    db.UserShoppingCarts
                        .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId)
                        .Quantity++;

                    db.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public bool DecreaseQuantity(string userId, int productId)
        {
            if (db.UserShoppingCarts
                .Any(usc => usc.UserId == userId && usc.ProductId == productId))
            {
                if (db.UserShoppingCarts.FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId).Quantity
                    > 1)
                {
                    db.UserShoppingCarts
                        .FirstOrDefault(usc => usc.UserId == userId && usc.ProductId == productId)
                        .Quantity--;

                    db.SaveChanges();
                }

                return true;
            }

            return false;
        }
    }
}