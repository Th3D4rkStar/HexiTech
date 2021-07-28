namespace HexiTech.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using HexiTech.Services.Products.Models;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 6;

        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProducts { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();

        [Display(Name = "ProductType")]
        public int ProductTypeId { get; init; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}