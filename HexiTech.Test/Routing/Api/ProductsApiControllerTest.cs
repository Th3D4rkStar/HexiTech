namespace HexiTech.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HexiTech.Controllers.Api;
    using HexiTech.Models.Api.Products;

    public class ProductsApiControllerTest
    {
        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/api/products")
                .To<ProductsApiController>(c => c.All(
                    With.Any<AllProductsApiRequestModel>()));
    }
}