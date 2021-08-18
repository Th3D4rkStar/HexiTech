namespace HexiTech.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;
    using HexiTech.Data.Models;

    public class AboutControllerTest
    {
        [Fact]
        public void AboutRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/About/About")
                .To<AboutController>(c => c.About());

        [Fact]
        public void ContactUsGetRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/About/ContactUs")
                .To<AboutController>(c => c.ContactUs());

        [Fact]
        public void ContactUsPostRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/About/ContactUs")
                    .WithMethod(HttpMethod.Post))
                .To<AboutController>(c => c.ContactUs(With.Any<Feedback>()));
    }
}