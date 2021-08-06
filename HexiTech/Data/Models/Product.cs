namespace HexiTech.Data.Models
{
    using Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; }

        [StringLength(SeriesMaxLength)]
        public string Series { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Range(0, 1000)]
        public int Quantity { get; set; }

        [Required]
        public ProductAvailability Availability { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        public string Specifications { get; set; }

        public IEnumerable<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    }
}