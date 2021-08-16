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

        public ActionResult All()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Finalize()
        {
            return View();
        }
    }
}