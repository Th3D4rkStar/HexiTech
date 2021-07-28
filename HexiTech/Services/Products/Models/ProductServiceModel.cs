namespace HexiTech.Services.Products.Models
{
    public class ProductServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; set; }

        public string Series { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string ProductTypeName { get; set; }

        public string CategoryName { get; set; }
    }
}