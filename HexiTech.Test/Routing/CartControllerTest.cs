namespace HexiTech.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;

    public class CartControllerTest
    {
        [Fact]
        public void CartShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Cart/Cart?userId=1")
                .To<CartController>(c => c.Cart());

        [Fact]
        public void AddToCartShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Cart/AddToCart/1?quantity=1")
                .To<CartController>(c => c.AddToCart(1, 1));

        [Fact]
        public void RemoveShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Cart/RemoveItem/1")
                .To<CartController>(c => c.RemoveItem(1));

        [Fact]
        public void IncreaseQuantityShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Cart/IncreaseQuantity/1")
                .To<CartController>(c => c.IncreaseQuantity(1));

        [Fact]
        public void DecreaseQuantityShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Cart/DecreaseQuantity/1")
                .To<CartController>(c => c.DecreaseQuantity(1));
    }
}