using HexiTech.Infrastructure.Extensions;

namespace HexiTech.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data;
    using Models.Orders;
    using Services.Orders;

    using static WebConstants;

    public class OrdersController : Controller
    {
        private readonly HexiTechDbContext db;
        private readonly IOrderService orders;

        public OrdersController(HexiTechDbContext db, IOrderService orders)
        {
            this.db = db;
            this.orders = orders;
        }

        [Authorize]
        public ActionResult OrdersList()
        {
            return View();
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(OrderFormModel order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            var userId = User.Id();

            var finalized = this.orders.Finalize(userId,
                order.FirstName,
                order.LastName,
                order.CompanyName,
                order.Country,
                order.Postcode,
                order.City,
                order.Province,
                order.Address,
                order.Address2,
                order.PhoneNumber,
                order.Email,
                order.AdditionalInformation);

            if (!finalized)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Your order was received! You can check your order details here - in your \"Orders\" tab.";

            return RedirectToAction(nameof(OrdersList));
        }
    }
}