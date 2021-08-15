namespace HexiTech.Services.Cart.Models
{
    using System.ComponentModel.DataAnnotations;

    using HexiTech.Data.Models;

    public class CartItemServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}