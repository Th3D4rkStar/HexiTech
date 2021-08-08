namespace HexiTech.Services.Products
{
    using System.Collections.Generic;
    using Data.Enums;
    using HexiTech.Models;
    using Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.TimeAdded,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true);

        public IEnumerable<LatestProductServiceModel> Latest();

        int Create(
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
            string specifications);

        int CreateReview(
            ReviewRatings rating,
            string content,
            string author,
            int productId
        );

        ProductDetailsServiceModel Details(int productId);

        bool Edit(
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
            string specifications);

        void ChangeVisibility(int productId);

        IEnumerable<string> AllBrands();

        IEnumerable<ProductCategoryServiceModel> AllCategories();

        IEnumerable<ProductTypeServiceModel> AllProductTypes();

        public bool CategoryExists(int categoryId);

        public bool ProductTypeExists(int productTypeId);
    }
}