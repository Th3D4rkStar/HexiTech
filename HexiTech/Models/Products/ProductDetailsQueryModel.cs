using System.ComponentModel.DataAnnotations;

namespace HexiTech.Models.Products
{
    using HexiTech.Data.Models;

    public class ProductDetailsQueryModel
    {
        public int Id { get; init; }
        
        public string Brand { get; set; }
        
        public string Series { get; set; }
        
        public string Model { get; set; }
        
        public string ImageUrl { get; set; }

        [Display(Name = "ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public decimal Price { get; set; }
        
        public ProductAvailability Availability { get; set; }
        
        public string Description { get; set; }

        public string Specifications { get; set; }
    }
}