namespace HexiTech.Services.Orders
{
    using System.Collections.Generic;
    using HexiTech.Data.Models;

    public interface IOrderService
    {
        public int Finalize(string userId, string firstName, string lastName, string companyName, string country, string postCode, string city,
            string province, string address, string address2, string phoneNumber, string email, string additionalInformation);

        public bool SubtractProductStock(string userId);

        public bool ClearCart(string userId);

        public bool AddOrderToList(string userId, int orderId);

        public IEnumerable<Order> GetUserOrders(string userId);
    }
}