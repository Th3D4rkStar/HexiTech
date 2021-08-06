namespace HexiTech.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Data;
    using Models.Products;
    using Services.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly HexiTechDbContext db;
        private readonly IMapper mapper;

        public ProductsController(IProductService products, HexiTechDbContext db, IMapper mapper)
        {
            this.products = products;
            this.db = db;
            this.mapper = mapper;
        }

        // GET: ProductsController
        public ActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage
            );

            var productBrands = this.products.AllBrands();

            query.Brands = productBrands;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;
            query.Categories = this.GetProductCategories();
            query.ProductTypes = this.GetProductTypes();

            return View(query);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            var product = this.products.Details(id);

            return this.View(product);
        }

        [HttpPost]
        public ActionResult Details(ProductReviewFormModel review)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details");
            }

            this.products.CreateReview(
                review.Rating,
                review.Content,
                review.Author,
                review.ProductId);

            return RedirectToAction("Details");
        }

        // GET: ProductsController/Add
        [Authorize]
        public ActionResult Add()
        {
            return View(new ProductFormModel
            {
                Categories = this.GetProductCategories(),
                ProductTypes = this.GetProductTypes()
            });
        }

        // POST: ProductsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Add(ProductFormModel product)
        {
            if (!this.db.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            this.products.Create(
                product.Brand,
                product.Series,
                product.Model,
                product.ImageUrl,
                product.ProductTypeId,
                product.CategoryId,
                product.Price,
                product.Quantity,
                product.Availability,
                product.Description,
                product.Specifications);

            try
            {
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetCascadeCategories()
        {
            var cat = this.db
                .Categories
                .Select(c => new ProductCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return Json(cat);
        }
        public JsonResult GetCascadeTypes(int catId)
        {
            var types = db.ProductTypes.Where(c => c.CategoryId == catId).ToList();

            return Json(types);
        }

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.db
                .Categories
                .Select(c => new ProductCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<ProductTypeViewModel> GetProductTypes()
            => this.db
                .ProductTypes
                .Select(pt => new ProductTypeViewModel()
                {
                    Id = pt.Id,
                    Name = pt.Name,
                    CategoryId = pt.CategoryId,
                    Category = pt.Category
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