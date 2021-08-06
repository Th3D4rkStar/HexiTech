namespace HexiTech.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Data.Enums;
    using static Data.DataConstants.Product;

    public class ProductFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; }

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
        [Range(1, 3)]
        public ProductAvailability Availability { get; set; }

        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Range(0, 1000)]
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();

        [Display(Name = "ProductType")]
        public int ProductTypeId { get; init; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}