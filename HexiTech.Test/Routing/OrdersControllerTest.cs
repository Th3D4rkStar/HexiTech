namespace HexiTech.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;
    using Models.Orders;

    public class OrdersControllerTest
    {
        [Fact]
        public void OrdersListShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Orders/OrdersList?userId=1")
                .To<OrdersController>(c => c.OrdersList());

        [Fact]
        public void CheckoutGetShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Orders/Checkout")
                .To<OrdersController>(c => c.Checkout());

        [Fact]
        public void CheckoutPostShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Orders/Checkout")
                    .WithMethod(HttpMethod.Post)).To<OrdersController>(c => c.Checkout(
                    With.Any<OrderFormModel>()));
    }
}