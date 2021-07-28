namespace HexiTech.Services.Products.Models
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Description { get; init; }

        public string Specifications { get; set; }

        public int ProductTypeId { get; init; }

        public int CategoryId { get; init; }
    }
}