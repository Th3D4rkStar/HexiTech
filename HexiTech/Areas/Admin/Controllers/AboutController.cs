namespace HexiTech.Areas.Admin.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    using Data;

    using static WebConstants;

    public class AboutController : AdminController
    {
        private readonly HexiTechDbContext db;

        public AboutController(HexiTechDbContext db)
        {
            this.db = db;
        }

        public IActionResult Messages()
        {
            var feedback = db.Feedbacks.ToList();

            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData[FailureMessageKey] = "Invalid message Id.";

                return RedirectToAction(nameof(Messages));
            }

            var feedback = db.Feedbacks.FirstOrDefault(fb => fb.Id == id);

            db.Feedbacks.Remove(feedback);
            db.SaveChanges();

            TempData[GlobalMessageKey] = "Message successfully deleted!";

            return RedirectToAction(nameof(Messages));
        }
    }
}