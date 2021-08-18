namespace HexiTech.Data.Models
{
    using System;

    public class UserOrdersList
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    }
}