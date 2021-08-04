namespace HexiTech.Services.Products.Models
{
    using System.Collections.Generic;
    using HexiTech.Data.Models;

    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Description { get; init; }

        public string Specifications { get; set; }

        public List<ProductReview> ProductReviews { get; set; }

        public int ProductTypeId { get; init; }

        public int CategoryId { get; init; }
    }
}