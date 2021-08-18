﻿namespace HexiTech.Test.Pipeline
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}