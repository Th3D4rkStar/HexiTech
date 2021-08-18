namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Data;
    using HexiTech.Data.Models;

    using static WebConstants;

    public class AboutController : Controller
    {
        private readonly HexiTechDbContext db;

        public AboutController(HexiTechDbContext db)
        {
            this.db = db;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for your feedback! Your opinion is important to us!";

            return RedirectToAction("Index", "Home");
        }
    }
}