namespace HexiTech.Test.Routing.Admin
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using ProductsController = Areas.Admin.Controllers.ProductsController;

    public class ProductsControllerTest
    {
        [Fact]
        public void ManageShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Products/Manage")
                .To<ProductsController>(c => c.Manage());

        [Fact]
        public void ChangeVisibilityShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Products/ChangeVisibility/1")
                .To<ProductsController>(c => c.ChangeVisibility(1));
    }
}