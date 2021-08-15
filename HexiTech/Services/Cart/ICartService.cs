namespace HexiTech.Services.Cart
{
    using System.Collections.Generic;

    using Models;

    public interface ICartService
    {
        bool AddToCart(string userId, int productId);

        bool Remove(string userId, int cartItemId);

        int Count(string userId);

        IEnumerable<CartItemServiceModel> GetCartItemsByUser(string userId);
    }
}