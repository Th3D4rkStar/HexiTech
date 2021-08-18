namespace HexiTech.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;
    using Models.Products;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/All")
                .To<ProductsController>(c => c.All(
                    With.Any<AllProductsQueryModel>()));

        [Fact]
        public void DetailsShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Details/1")
                .To<ProductsController>(c => c.Details(1));

        [Fact]
        public void AddGetShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Add")
                .To<ProductsController>(c => c.Add());

        [Fact]
        public void AddPostShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Products/Add")
                    .WithMethod(HttpMethod.Post)).To<ProductsController>(c => c.Add(
                    With.Any<ProductFormModel>()));
        
        [Fact]
        public void EditGetShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Edit/1")
                .To<ProductsController>(c => c.Edit(1));

        [Fact]
        public void EditPostShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Products/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.Edit(
                    1, With.Any<ProductFormModel>()));

        [Fact]
        public void DeleteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Delete/1")
                .To<ProductsController>(c => c.Delete(1));
    }
}