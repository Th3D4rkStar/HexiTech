namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Data;

    public class OrdersController : Controller
    {
        private readonly HexiTechDbContext db;

        public OrdersController(HexiTechDbContext db)
        {
            this.db = db;
        }

        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            return View();
        }

        public IActionResult GetCartId()
        {
            return View();
        }
    }
}