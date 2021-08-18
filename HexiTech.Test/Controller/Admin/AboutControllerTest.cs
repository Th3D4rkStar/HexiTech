namespace HexiTech.Test.Controller.Admin
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HexiTech.Data.Models;
    using HexiTech.Areas.Admin.Controllers;

    public class AboutControllerTest
    {
        [Fact]
        public void MessagesShouldRequireAdminAuthorizationAndReturnViewWithCorrectModel()
            => MyController<AboutController>
                .Instance()
                .WithUser(user => user
                    .InRole("Admin"))
                .Calling(c => c.Messages())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Feedback>>());
    }
}