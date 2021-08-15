namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Products;
    using Services.Cart;
    using Data;

    public class CartController : Controller
    {
        private readonly HexiTechDbContext db;
        private readonly IProductService products;
        private readonly ICartService cart;

        public CartController(HexiTechDbContext db, IProductService products, ICartService cart)
        {
            this.db = db;
            this.products = products;
            this.cart = cart;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction();
            }

            var cartItems = this.cart.GetCartItemsByUser(User.Id());

            foreach (var item in cartItems)
            {
                item.Name = item.Product.Series != null
                    ? $"{item.Product.Brand} {item.Product.Series} {item.Product.Model}"
                    : $"{item.Product.Brand} + {item.Product.Model}";
            }

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var isAdded = this.cart.AddToCart(User.Id(), id);

            if (!isAdded)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Cart));
        }
    }
}