namespace HexiTech.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void PrivacyRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Privacy")
                .To<HomeController>(c => c.Privacy());

        [Fact]
        public void ErrorRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}