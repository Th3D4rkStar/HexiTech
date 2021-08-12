namespace HexiTech.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductsOrders
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}