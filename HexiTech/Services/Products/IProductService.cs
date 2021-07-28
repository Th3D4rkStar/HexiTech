namespace HexiTech.Services
{
    using System.Collections.Generic;
    using HexiTech.Data.Models;
    using Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        int Create(
            string brand,
            string series,
            string model,
            string imageUrl,
            int productTypeId,
            int categoryId,
            decimal price,
            ProductAvailability availability,
            string description,
            string specifications);

        bool Edit(
            int productId,
            string brand,
            string series,
            string model,
            string imageUrl,
            int productTypeId,
            int categoryId,
            decimal price,
            ProductAvailability availability,
            string description,
            string specifications);

        IEnumerable<string> AllBrands();
        //IEnumerable<ProductCategoryServiceModel>
    }
}