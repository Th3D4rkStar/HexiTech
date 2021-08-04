namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}