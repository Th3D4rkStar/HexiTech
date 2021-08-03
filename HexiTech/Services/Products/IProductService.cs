namespace HexiTech.Services.Products
{
    using System.Collections.Generic;
    using HexiTech.Data.Models;
    using HexiTech.Models;
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
            ProductAvailability availability,
            string description,
            string specifications);

        IEnumerable<string> AllBrands();
    }
}