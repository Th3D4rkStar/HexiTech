namespace HexiTech.Services.Cart
{
    using System.Collections.Generic;

    using Models;

    public interface ICartService
    {
        bool AddToCart(string userId, int productId, int quantity);

        bool Remove(string userId, int cartItemId);

        IEnumerable<CartItemServiceModel> GetCartItemsByUser(string userId);
        int Count(string userId);

        public bool IncreaseQuantity(string userId, int productId);

        public bool DecreaseQuantity(string userId, int productId);
    }
}