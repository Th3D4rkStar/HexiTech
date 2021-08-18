namespace HexiTech.Test.Controller
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}