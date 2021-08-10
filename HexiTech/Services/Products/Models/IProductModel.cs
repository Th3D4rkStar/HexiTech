namespace HexiTech.Services.Products.Models
{
    public interface IProductModel
    {
        string Brand { get; }

        public string Series { get; }

        string Model { get; }

        decimal Price { get; }
    }
}