using System.Net;
using HexiTech.Data.Models;

namespace HexiTech.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Extensions;
    using AutoMapper;
    using Data;
    using HexiTech.Services.Products.Models;
    using Models.Products;
    using Services.Products;

    using static WebConstants;

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

        public IActionResult All([FromQuery] AllProductsQueryModel query)
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

        public IActionResult Details(int id)
        {
            var product = this.products.Details(id);

            return this.View(product);
        }

        [HttpPost]
        public IActionResult AddReview(ProductReviewFormModel review)
        {
            if (!ModelState.IsValid)
            {
                TempData[FailureMessageKey] = "Incomplete review!";

                return RedirectToAction(nameof(Details), new { id = review.ProductId });
            }

            this.products.CreateReview(
                review.Rating,
                review.Content,
                review.Author,
                review.ProductId);

            TempData[GlobalMessageKey] = "Your review was published!";

            return RedirectToAction(nameof(Details), new { id = review.ProductId });
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new ProductFormModel
            {
                Categories = this.GetProductCategories(),
                ProductTypes = this.GetProductTypes()
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductFormModel product)
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

            var productId = this.products.Create(
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

            TempData[GlobalMessageKey] = "The product was added!";

            return RedirectToAction(nameof(Details), new { id = productId });
        }

        public JsonResult GetCascadeCategories()
        {
            var cat = this.db
                .Categories
                .Select(c => new ProductCategoryServiceModel()
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

        private IEnumerable<ProductCategoryServiceModel> GetProductCategories()
            => this.db
                .Categories
                .Select(c => new ProductCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<ProductTypeServiceModel> GetProductTypes()
            => this.db
                .ProductTypes
                .Select(pt => new ProductTypeServiceModel()
                {
                    Id = pt.Id,
                    Name = pt.Name,
                    CategoryId = pt.CategoryId,
                    Category = pt.Category
                })
                .ToList();

        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            var product = this.products.Details(id);

            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);

            productForm.Categories = this.products.AllCategories();
            productForm.ProductTypes = this.products.AllProductTypes();

            return View(productForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }
            if (!this.products.ProductTypeExists(product.ProductTypeId))
            {
                this.ModelState.AddModelError(nameof(product.ProductTypeId), "Product type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();
                product.ProductTypes = this.products.AllProductTypes();

                return View(product);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.products.Edit(
                id,
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

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Your product was edited!";

            return RedirectToAction(nameof(Details), new { id });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData[FailureMessageKey] = "Invalid product Id.";

                return RedirectToAction(nameof(All));
            }

            var deleted = products.Delete(id);

            if (!deleted)
            {
                TempData[FailureMessageKey] = "Product not found.";

                return RedirectToAction(nameof(All));
            }

            TempData[GlobalMessageKey] = "Product successfully deleted!";

            return RedirectToAction(nameof(All));
        }
    }
}