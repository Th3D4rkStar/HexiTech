namespace HexiTech.Services.Products.Models
{
    public class LatestProductServiceModel : IProductModel
    {
        public int Id { get; init; }

        public string Brand { get; }

        public string Series { get; set; }

        public string Model { get; }

        public string ImageUrl { get; set; }

        public decimal Price { get; }
    }
}