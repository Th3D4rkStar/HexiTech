namespace HexiTech.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using HexiTech.Data.Models;
    using static Data.DataConstants.Product;

    public class AddProductFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(SeriesMaxLength)]
        public string Series { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }

        [Required]
        public string Specifications { get; set; }

        [Required]
        public ProductAvailability Availability { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();

        [Display(Name = "ProductType")]
        public int ProductTypeId { get; init; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}