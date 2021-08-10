namespace HexiTech.Services.Products.Models
{
    public class LatestProductServiceModel : IProductModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Series { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }
    }
}