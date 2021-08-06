namespace HexiTech.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static DataConstants.ProductReview;

    public class ProductReview
    {
        public int Id { get; init; }

        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        [Required]
        public ReviewRatings Rating { get; set; }

        [Required]
        public string ReviewDate { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}