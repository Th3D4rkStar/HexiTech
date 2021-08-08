namespace HexiTech.Services.Products.Models
{
    using System.Collections.Generic;

    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductServiceModel> Products { get; init; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; } = new List<ProductCategoryServiceModel>();

        public IEnumerable<ProductTypeServiceModel> ProductTypes { get; set; } = new List<ProductTypeServiceModel>();
    }
}