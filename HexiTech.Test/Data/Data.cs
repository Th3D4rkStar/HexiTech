using System;
using System.Collections.Generic;
using HexiTech.Models.Products;

namespace HexiTech.Test.Data
{
    using HexiTech.Data.Enums;
    using HexiTech.Data.Models;

    public static class Data
    {
        public static Product GetProduct()
            => new Product
            {
                Brand = "Apple",
                Series = "iPhone",
                Model = "12",
                ImageUrl = "https://localhost:5001/img/Products/apple-iPhone-12.jpg",
                ProductTypeId = 4,
                CategoryId = 1,
                Price = 1200M,
                Quantity = 10,
                Availability = ProductAvailability.AtWarehouse,
                Description =
                    "Blast past fast. Say hello to the fastest smartphone chip ever. 5G speed. An advanced dual-camera system. Ceramic Shield for 4x better drop performance. And a brighter, more colorful OLED display. iPhone 12 is a total powerhouse — in two great sizes.",
                Specifications =
                    "Network Speed - 5G capable | Screen size - 6.1\" Super Retina XDR OLED display (460 ppi) | Camera - Dual 12MP rear cameras, 12MP front camera | Operating System - iOS 14 | Processor - A14 Bionic Chip | SIM Card - Dual SIM (Nano and eSIM) | Battery Life - Up to 17 hour video, 65 hour audio playback | Memory - 64GB, 128GB or 256GB",
                IsPublic = true
            };

        public static Category GetCategory()
            => new Category
            {
                Id = 1,
                Name = "Mobile phones and tablets"
            };

        public static ProductType GetProductType()
            => new ProductType
            {
                Id = 4,
                Name = "Smartphones"
            };

        public static ProductFormModel GetValidProductFormModel()
            => new ProductFormModel
            {
                Brand = "Apple",
                Series = "iPhone",
                Model = "12",
                ImageUrl = "https://localhost:5001/img/Products/apple-iPhone-12.jpg",
                Description =
                    "Blast past fast. Say hello to the fastest smartphone chip ever. 5G speed. An advanced dual-camera system. Ceramic Shield for 4x better drop performance. And a brighter, more colorful OLED display. iPhone 12 is a total powerhouse — in two great sizes.",
                Specifications =
                    "Network Speed - 5G capable | Screen size - 6.1\" Super Retina XDR OLED display (460 ppi) | Camera - Dual 12MP rear cameras, 12MP front camera | Operating System - iOS 14 | Processor - A14 Bionic Chip | SIM Card - Dual SIM (Nano and eSIM) | Battery Life - Up to 17 hour video, 65 hour audio playback | Memory - 64GB, 128GB or 256GB",
                Availability = ProductAvailability.AtWarehouse,
                Price = 1200M,
                Quantity = 10,
                CategoryId = 1,
                ProductTypeId = 4,
            };

        public static ProductReview GetProductReview()
            => new ProductReview
            {
                Author = "TestAuthor",
                Content = "Great product!",
                Rating = ReviewRatings.Excellent,
                ReviewDate = DateTime.UtcNow.ToString(),
                ProductId = 1
            };

        public static Order GetOrder()
            => new Order
            {
                UserId = "TestId",
                DateCreated = DateTime.UtcNow.ToString(),
                TotalPrice = 1206.90M,
                DeliveryDate = DateTime.UtcNow.AddDays(5).ToString(),
                IsFulfilled = false,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                CompanyName = "TestCompanyName",
                Country = "TestCountry",
                Postcode = "TestPostCode",
                City = "TestCity",
                Province = "TestProvince",
                Address = "TestAddress",
                Address2 = "TestAddress2",
                PhoneNumber = "0000000000",
                Email = "test@test.com",
                AdditionalInformation = "TestAdditionalInformation"
            };

        public static UserShoppingCart GetUserShoppingCart()
            => new UserShoppingCart
            {
                UserId = "TestId",
                ProductId = 1
            };

        public static UserOrdersList GetUserOrdersList()
            => new UserOrdersList
            {
                UserId = "TestId",
                OrderId = 1
            };
    }
}