namespace HexiTech.Data.Models
{
    public class UserOrdersList
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}