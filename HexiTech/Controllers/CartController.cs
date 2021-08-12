namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Data;

    public class CartController : Controller
    {
        private readonly HexiTechDbContext db;

        public const string CartSessionKey = "CartId";

        public CartController(HexiTechDbContext db)
        {
            this.db = db;
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}