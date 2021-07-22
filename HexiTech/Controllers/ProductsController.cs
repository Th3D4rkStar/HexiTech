using System.Collections.Generic;
using System.Linq;
using HexiTech.Models.Products;

namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Data;

    public class ProductsController : Controller
    {
        private readonly HexiTechDbContext db;

        public ProductsController(HexiTechDbContext db)
        {
            this.db = db;
        }

        // GET: ProductsController
        public ActionResult All()
        {
            return View();
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Add
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        // POST: ProductsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Add(AddProductFormModel product)
        {
            if (!this.db.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetCarCategories();

                return View(product);
            }

            try
            {
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<ProductCategoryViewModel> GetCarCategories()
            => this.db
                .Categories
                .Select(c => new ProductCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }
    }
}