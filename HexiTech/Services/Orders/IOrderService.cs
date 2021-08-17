namespace HexiTech.Services.Orders
{
    public interface IOrderService
    {
        public bool Finalize(string userId, string firstName, string lastName, string companyName, string country, string postCode, string city,
            string province, string address, string address2, string phoneNumber, string email, string additionalInformation);

        public bool SubtractProductStock(string userId);

        public bool ClearCart(string userId);
    }
}