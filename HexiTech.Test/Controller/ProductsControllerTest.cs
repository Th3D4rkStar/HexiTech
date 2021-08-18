using System.Linq;
using HexiTech.Data.Models;

namespace HexiTech.Test.Controller
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Data;
    using Controllers;
    using Models.Products;
    using HexiTech.Services.Products.Models;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
            => MyController<ProductsController>
                .Instance()
                .WithData(Data.GetProduct())
                .Calling(c => c.All(
                    new AllProductsQueryModel
                    {
                        CurrentPage = 1,
                        Products = new List<ProductServiceModel>()
                    }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllProductsQueryModel>());

        [Fact]
        public void AddGetShouldRequireAuthorizationAndReturnViewWithCorrectModel()
            => MyController<ProductsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductFormModel>());

        [Theory]
        [InlineData("Apple", 1200)]
        public void AddPostShouldRequireAuthorizionAndRedirectWithValidModel(string brand, decimal price)
            => MyController<ProductsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(Data.GetCategory(), Data.GetProductType()))
                .Calling(c => c.Add(
                    Data.GetValidProductFormModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Product>(products => products
                        .Any(p =>
                            p.Brand == brand &&
                            p.Price == price)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.Details(1)));

        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnViewWithCorrectModel(int id)
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Details(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductDetailsServiceModel>());
    }
}