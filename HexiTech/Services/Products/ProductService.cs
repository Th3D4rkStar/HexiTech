namespace HexiTech.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.Globalization;
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

        public ProductQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.TimeAdded,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var productsQuery = this.db.Products
                .Where(p => !publicOnly || p.IsPublic);

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

            var products = GetProducts(productsQuery)
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage);

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public IEnumerable<LatestProductServiceModel> Latest()
            => this.db
                .Products
                .Where(p => p.IsPublic)
                .OrderByDescending(p => p.Id)
                .ProjectTo<LatestProductServiceModel>(this.mapper)
                .Take(6)
                .ToList();

        public int Create(string brand, string series, string model, string imageUrl, int productTypeId, int categoryId, decimal price,
            int quantity, ProductAvailability availability, string description, string specifications)
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
                Quantity = quantity,
                Availability = availability,
                Description = description,
                Specifications = specifications,
                IsPublic = false
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

        public ProductDetailsServiceModel Details(int productId) =>
            this.db
                .Products
                .Where(p => p.Id == productId)
                .ProjectTo<ProductDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Edit(
            int productId,
            string brand,
            string series,
            string model,
            string imageUrl,
            int productTypeId,
            int categoryId,
            decimal price,
            int quantity,
            ProductAvailability availability,
            string description,
            string specifications)
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
            productData.Quantity = quantity;
            productData.Availability = availability;
            productData.Description = description;
            productData.Specifications = specifications;

            this.db.SaveChanges();

            return true;
        }

        public void ChangeVisibility(int productId)
        {
            var product = this.db.Products.Find(productId);

            product.IsPublic = !product.IsPublic;

            this.db.SaveChanges();
        }

        public IEnumerable<string> AllBrands()
            => this.db
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<ProductCategoryServiceModel> AllCategories()
            => this.db
                .Categories
                .ProjectTo<ProductCategoryServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<ProductTypeServiceModel> AllProductTypes()
            => this.db
                .ProductTypes
                .ProjectTo<ProductTypeServiceModel>(this.mapper)
                .ToList();

        public bool CategoryExists(int categoryId)
            => this.db
                .Categories
                .Any(c => c.Id == categoryId);

        public bool ProductTypeExists(int productTypeId)
            => this.db
                .ProductTypes
                .Any(c => c.Id == productTypeId);

        private IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
                .ProjectTo<ProductServiceModel>(this.mapper)
                .ToList();
    }
}