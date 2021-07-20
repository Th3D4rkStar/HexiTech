namespace HexiTech.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.ProductType;

    public class ProductType
    {
        public int Id { get; init; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}