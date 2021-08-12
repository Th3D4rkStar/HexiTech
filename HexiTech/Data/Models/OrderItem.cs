namespace HexiTech.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderItem
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string ItemType { get; set; }

        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
    }
}