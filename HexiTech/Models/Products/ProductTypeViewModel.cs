namespace HexiTech.Models.Products
{
    using HexiTech.Data.Models;
    public class ProductTypeViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}