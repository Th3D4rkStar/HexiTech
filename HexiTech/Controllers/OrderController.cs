namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Data;

    public class OrderController : Controller
    {
        private readonly HexiTechDbContext db;

        public const string CartSessionKey = "CartId";

        public OrderController(HexiTechDbContext db)
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

        //public string GetCartId()
        //{
        //    if (HttpContext.Current.Session[CartSessionKey] == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
        //        {
        //            HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
        //        }
        //        else
        //        {
        //            Guid tempCartId = Guid.NewGuid();
        //            HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
        //        }
        //    }
        //    return HttpContext.Current.Session[CartSessionKey].ToString();
        //}
    }
}