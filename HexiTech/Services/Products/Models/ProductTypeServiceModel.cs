using HexiTech.Data.Models;

namespace HexiTech.Services.Products.Models
{
    public class ProductTypeServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}