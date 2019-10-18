using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OnlineOrderManagerController : Controller
    {
        IOnlineOrderService orderService;

        public OnlineOrderManagerController(IOnlineOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<OnlineOrder> orders = orderService.GetOnlineOrderList();
            return View(orders);
        }

        public ActionResult UpdateOnlineOrder(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            OnlineOrder order = orderService.GetOnlineOrder(Id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOnlineOrder(OnlineOrder updatedOrder, string Id)
        {
            // only allow update for OrderStatus 
            OnlineOrder order = orderService.GetOnlineOrder(Id);
            order.OrderStatus = updatedOrder.OrderStatus;
            orderService.UpdateOnlineOrder(order);

            return RedirectToAction("Index");
        }
    }
}