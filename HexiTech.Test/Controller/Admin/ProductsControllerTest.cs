namespace HexiTech.Test.Controller.Admin
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HexiTech.Areas.Admin.Controllers;
    using HexiTech.Services.Products.Models;

    public class ProductsControllerTest
    {
        [Fact]
        public void ManageShouldRequireAdminAuthorizationAndReturnViewWithCorrectModel()
            => MyController<ProductsController>
                .Instance()
                .WithUser(user => user
                    .InRole("Admin"))
                .Calling(c => c.Manage())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ProductServiceModel>>());
    }
}