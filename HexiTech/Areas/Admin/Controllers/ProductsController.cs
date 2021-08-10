using HexiTech.Infrastructure.Extensions;

namespace HexiTech.Areas.Admin.Controllers
{
    using Services.Products;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products) => this.products = products;

        public IActionResult Manage()
        {
            var products = this.products
                .All(publicOnly: false)
                .Products;

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData[FailureMessageKey] = "Invalid product Id.";

                return RedirectToAction(nameof(Manage));
            }

            var deleted = products.Delete(id);

            if (!deleted)
            {
                TempData[FailureMessageKey] = "Product not found.";

                return RedirectToAction(nameof(Manage));
            }

            TempData[GlobalMessageKey] = "Product successfully deleted!";

            return RedirectToAction(nameof(Manage));
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.products.ChangeVisibility(id);

            return RedirectToAction(nameof(Manage));
        }
    }
}