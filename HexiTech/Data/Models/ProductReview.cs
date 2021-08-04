namespace HexiTech.Data.Models
{
    using System;
    using Enums;

    public class ProductReview
    {
        public int Id { get; init; }

        public string Author { get; set; }

        public string Content { get; set; }

        public ReviewRatings Rating { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}