namespace HexiTech.Services.Orders.Models
{
    using System.ComponentModel.DataAnnotations;

    using HexiTech.Data.Models;

    public class OrderListItemServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ItemType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}