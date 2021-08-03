﻿namespace HexiTech.Services.Products.Models
{
    using HexiTech.Data.Models;
    public class ProductServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; set; }

        public string Series { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public ProductAvailability Availability { get; set; }

        public string ProductTypeName { get; set; }

        public string CategoryName { get; set; }
    }
}