namespace HexiTech.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using HexiTech.Data.Models;
    using HexiTech.Models;
    using Models;

    public class ProductService : IProductService
    {
        private readonly HexiTechDbContext db;

        public ProductService(HexiTechDbContext db)
            => this.db = db;

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
                ProductSorting.DateAdded or _ => productsQuery.OrderByDescending(p => p.Id)
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