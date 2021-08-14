namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using HexiTech.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Services.Products;
    using Data;

    public class CartController : Controller
    {
        private readonly HexiTechDbContext db;
        private readonly IProductService products;
        public List<CartItem> cartItems;
        public decimal total;

        public CartController(HexiTechDbContext db, IProductService products)
        {
            this.db = db;
            this.products = products;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cartItems");

            if (cartItems != null)
            {
                total = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);
            }

            var cart = new ShoppingCart
            {
                CartItems = cartItems
            };

            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var productToAdd = products.Details(id);

            cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cartItems");

            if (cartItems == null)
            {
                cartItems = new List<CartItem>
                {
                    new()
                    {
                        Name = productToAdd.Series != null
                            ? $"{productToAdd.Brand} {productToAdd.Series} {productToAdd.Model}"
                            : $"{productToAdd.Brand} {productToAdd.Model}",
                        Quantity = 1,
                        ProductId = id,
                        Product = db.Products.Find(id)
                    }
                };

                HttpContext.Session.SetObjectAsJson("cartItems", cartItems);
            }
            else
            {
                if (!cartItems.Exists(ci => ci.ProductId == id))
                {
                    cartItems.Add(new CartItem
                    {
                        Name = productToAdd.Series != null
                            ? $"{productToAdd.Brand} {productToAdd.Series} {productToAdd.Model}"
                            : $"{productToAdd.Brand} {productToAdd.Model}",
                        Quantity = 1,
                        ProductId = id,
                        Product = db.Products.Find(id)
                    });
                }
                else
                {
                    cartItems.FirstOrDefault(ci => ci.ProductId == id).Quantity++;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartItems", cartItems);
            }

            return RedirectToAction(nameof(Cart));
        }
    }
}