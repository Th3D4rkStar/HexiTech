namespace HexiTech.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Enums;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string DateCreated { get; set; }

        [Required]
        public DeliveryType DeliveryType { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public string DeliveryDate { get; set; }

        public bool IsFulfilled { get; set; }

        public string AdditionalInformation { get; set; }
    }
}