namespace HexiTech.Services.Orders
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Cart;
    using Data.Enums;
    using HexiTech.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly HexiTechDbContext db;
        private readonly ICartService cart;
        private readonly IConfigurationProvider mapper;

        public OrderService(HexiTechDbContext db, ICartService cart, IMapper mapper)
        {
            this.db = db;
            this.cart = cart;
            this.mapper = mapper.ConfigurationProvider;
        }

        public int Finalize(string userId, string firstName, string lastName, string companyName, string country,
            string postCode, string city,
            string province, string address, string address2, string phoneNumber, string email,
            string additionalInformation)
        {
            var deliveryDate = DateTime.UtcNow.AddDays(3);
            var list = db.UserShoppingCarts
                .Where(usc => usc.UserId == userId)
                .Include(usc => usc.Product)
                .ToList();

            if (list.Any(usc => usc.Product.Availability.GetDisplayName() == "At warehouse."))
            {
                deliveryDate = deliveryDate.AddDays(2);
            }

            var orderItems = cart.GetCartItemsByUser(userId);
            foreach (var item in orderItems)
            {
                item.Name = item.Product.Series != null
                    ? $"{item.Product.Brand} {item.Product.Series} {item.Product.Model}"
                    : $"{item.Product.Brand} + {item.Product.Model}";
            }

            var orderData = new Order
            {
                DateCreated = DateTime.UtcNow.ToString(),
                OrderItems = orderItems,
                TotalPrice = db.UserShoppingCarts
                    .Where(usc => usc.UserId == userId)
                    .Include(usc => usc.Product)
                    .ToList().Sum(usc => usc.Product.Price) + 6.90M,
                DeliveryDate = deliveryDate.ToString(),
                IsFulfilled = false,
                FirstName = firstName,
                LastName = lastName,
                CompanyName = companyName,
                Country = country,
                Postcode = postCode,
                City = city,
                Province = province,
                Address = address,
                Address2 = address2,
                PhoneNumber = phoneNumber,
                Email = email,
                AdditionalInformation = additionalInformation
            };

            db.Orders.Add(orderData);
            db.SaveChanges();

            SubtractProductStock(userId);

            ClearCart(userId);

            return orderData.Id;
        }

        public bool SubtractProductStock(string userId)
        {
            var orderItems = cart.GetCartItemsByUser(userId).ToList();

            foreach (var item in orderItems)
            {
                var product = db.Products.FirstOrDefault(p => p.Id == item.ProductId);

                product.Quantity -= item.Quantity;

                db.SaveChanges();

                if (product.Quantity == 0)
                {
                    product.Availability = ProductAvailability.OutOfStock;
                    db.SaveChanges();
                }
            }

            return true;
        }

        public bool ClearCart(string userId)
        {
            var userCart = db.UserShoppingCarts.Where(usc => usc.UserId == userId).ToList();

            db.UserShoppingCarts.RemoveRange(userCart);
            db.SaveChanges();

            return true;
        }

        public bool AddOrderToList(string userId, int orderId)
        {
            db.UserOrdersLists.Add(new UserOrdersList
            {
                UserId = userId,
                OrderId = orderId
            });

            db.SaveChanges();

            return true;
        }

        public IEnumerable<Order> GetUserOrders(string userId)
            => db.UserOrdersLists
                .Where(uol => uol.UserId == userId)
                .OrderByDescending(uol => uol.DateAdded)
                .ProjectTo<Order>(this.mapper)
                .ToList();
    }
}