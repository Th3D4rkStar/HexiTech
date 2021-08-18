namespace HexiTech.Test.Routing.Admin
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HexiTech.Areas.Admin.Controllers;

    public class AboutControllerTest
    {
        [Fact]
        public void MessagesShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/About/Messages")
                .To<AboutController>(c => c.Messages());

        [Fact]
        public void DeleteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/About/Delete/1")
                .To<AboutController>(c => c.Delete(1));
    }
}