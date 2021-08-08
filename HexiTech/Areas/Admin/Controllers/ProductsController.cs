using HexiTech.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace HexiTech.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products) => this.products = products;

        public IActionResult All()
        {
            var products = this.products
                .All(publicOnly: false)
                .Products;

            return View(products);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.products.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}