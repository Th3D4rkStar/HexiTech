namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}