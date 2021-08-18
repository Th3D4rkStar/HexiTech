namespace HexiTech.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data;
    using Models.Orders;
    using Services.Orders;
    using Infrastructure.Extensions;

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
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction();
            }

            var userId = User.Id();

            var orderItems = orders.GetUserOrders(userId).ToList();

            return View(orderItems);
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

            var orderId = this.orders.Finalize(userId,
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

            orders.AddOrderToList(userId, orderId);

            TempData[GlobalMessageKey] = "Your order was received! You can check your order details here - in your \"Orders\" tab.";

            return RedirectToAction(nameof(OrdersList));
        }
    }
}