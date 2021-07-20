namespace HexiTech.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MinLength(BrandMinLength)]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [MaxLength(SeriesMaxLength)]
        public string Series { get; set; }

        [Required]
        [MinLength(ModelMinLength)]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public ProductAvailability Availability { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        public string Specifications { get; set; }
    }
}