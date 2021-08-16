﻿namespace HexiTech.Services.Cart
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using HexiTech.Data.Models;

    public class CartService : ICartService
    {
        private readonly HexiTechDbContext db;
        private readonly IMapper mapper;

        public CartService(HexiTechDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool AddToCart(string userId, int productId, int quantity)
        {
            if (db.UserShoppingCarts.Any(usc => usc.UserId == userId && usc.ProductId == productId))
            {
                db.UserShoppingCarts
                    .FirstOrDefault(usc =>
                        usc.UserId == userId && usc.ProductId == productId)
                        .Quantity += quantity;

                db.SaveChanges();
            }
            else
            {
                db.UserShoppingCarts.Add(new UserShoppingCart()
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
                .ProjectTo<CartItemServiceModel>(this.mapper.ConfigurationProvider)
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