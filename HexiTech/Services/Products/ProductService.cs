﻿using System;
using System.Globalization;

namespace HexiTech.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using HexiTech.Data.Models;
    using HexiTech.Models;
    using Data.Enums;
    using Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class ProductService : IProductService
    {
        private readonly HexiTechDbContext db;
        private readonly IConfigurationProvider mapper;

        public ProductService(HexiTechDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        }

        public ProductQueryServiceModel All(string brand, string searchTerm, ProductSorting sorting, int currentPage,
            int productsPerPage)
        {
            var productsQuery = this.db.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                productsQuery = productsQuery.Where(p => p.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    (p.Brand + " " + p.Model).ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Price => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.BrandAndModel => productsQuery.OrderBy(p => p.Brand).ThenBy(p => p.Model),
                ProductSorting.TimeAdded or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Series = p.Series,
                    Model = p.Model,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ProductTypeName = p.ProductType.Name,
                    CategoryName = p.Category.Name
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public int Create(string brand, string series, string model, string imageUrl, int productTypeId, int categoryId, decimal price,
            ProductAvailability availability, string description, string specifications)
        {
            var productData = new Product
            {
                Brand = brand,
                Series = series,
                Model = model,
                ImageUrl = imageUrl,
                ProductTypeId = productTypeId,
                CategoryId = categoryId,
                Price = price,
                Availability = availability,
                Description = description,
                Specifications = specifications
            };

            this.db.Products.Add(productData);
            this.db.SaveChanges();

            return productData.Id;
        }

        public int CreateReview(ReviewRatings rating, string content, string author, int productId)
        {
            var reviewData = new ProductReview
            {
                Rating = rating,
                Content = content,
                Author = author,
                ProductId = productId,
                ReviewDate = DateTime.UtcNow.ToString("MMMM dd yyyy", CultureInfo.InvariantCulture)
            };

            this.db.ProductReviews.Add(reviewData);
            this.db.SaveChanges();

            return reviewData.Id;
        }

        public ProductDetailsServiceModel Details(int productId)
        {
            return this.db
                .Products
                .Where(p => p.Id == productId)
                .ProjectTo<ProductDetailsServiceModel>(this.mapper)
                .FirstOrDefault();
        }

        public bool Edit(int productId, string brand, string series, string model, string imageUrl, int productTypeId, int categoryId,
            decimal price, ProductAvailability availability, string description, string specifications)
        {
            var productData = this.db.Products.Find(productId);

            if (productData == null)
            {
                return false;
            }

            productData.Brand = brand;
            productData.Series = series;
            productData.Model = model;
            productData.ImageUrl = imageUrl;
            productData.ProductTypeId = productTypeId;
            productData.CategoryId = categoryId;
            productData.Price = price;
            productData.Availability = availability;
            productData.Description = description;
            productData.Specifications = specifications;

            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<string> AllBrands()
            => this.db
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
    }
}