namespace HexiTech.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using HexiTech.Data.Models;
    using Data.Enums;
    using static Data.DataConstants.ProductReview;

    public class ProductReviewFormModel
    {
        [Required]
        [Range(1, 5)]
        public ReviewRatings Rating { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}