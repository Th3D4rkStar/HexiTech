namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Products;

    public class HomeController : Controller
    {
        private readonly IProductService products;

        public HomeController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error() => View();
    }
}