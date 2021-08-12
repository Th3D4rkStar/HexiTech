namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using HexiTech.Data.Models;
    using Services.Products;
    using Data;

    public class CartController : Controller
    {
        private readonly HexiTechDbContext db;
        private readonly IProductService products;

        public CartController(HexiTechDbContext db, IProductService products)
        {
            this.db = db;
            this.products = products;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddItemToCart(int id, int quantity)
        {
            var productToAdd = products.Details(id);

            var cartItem = new CartItem
            {
                ImageUrl = productToAdd.ImageUrl,
                Name = productToAdd.Series != null
                    ? $"{productToAdd.Brand} {productToAdd.Series} {productToAdd.Model}"
                    : $"{productToAdd.Brand} {productToAdd.Model}",
                Availability = productToAdd.Availability,
                Quantity = quantity,
                Price = productToAdd.Price,
                ItemType = productToAdd.ProductTypeName,
                ProductId = id
            };

            return RedirectToAction(nameof(Cart));
        }
    }
}