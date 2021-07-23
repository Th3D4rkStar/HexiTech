namespace HexiTech.Services
{
    using System.Collections.Generic;
    using Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<string> AllProductBrands();
    }
}