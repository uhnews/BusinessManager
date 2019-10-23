using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using BusinessManager.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class BasketController : Controller
    {
        readonly IRepository<Customer> customers;
        readonly IBasketService basketService;
        readonly IOnlineOrderService orderService;
        readonly IRepository<Payment> paymentContext;

        public BasketController(
                                    IBasketService basketService, 
                                    IOnlineOrderService orderService, 
                                    IRepository<Customer> customers,
                                    IRepository<Payment> paymentContext
                               )
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.customers = customers;
            this.paymentContext = paymentContext;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            //POSTransactionSummaryViewModel summaryModel = posTransactionService.GetPOSTransactionSummary(this.HttpContext);
            BasketSummaryViewModel summaryModel = basketService.GetBasketSummary(this.HttpContext);

            // customer variable returns one Customer (whose email == current user's email)
            // i.e. the basket created is being assigned to the currently logged-in user
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                OnlineOrder order = new OnlineOrder()
                {
                    CustomerId = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CompanyName = customer.CompanyName,
                    Street = customer.Street,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    TotalItemCount = summaryModel.BasketCount,
                    TotalAmount = summaryModel.BasketTotal,
                    OnlineOrderItems = summaryModel.OnlineOrderItems
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(OnlineOrder order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

            bool validatedIfPOSAttendant = !User.IsInRole("POSAttendant") || (User.IsInRole("POSAttendant") && User.IsInRole("Client"));
            if (!validatedIfPOSAttendant)
            {
                return RedirectToAction("Error");
            }

            // process payment here
            HttpCookie cookie = this.HttpContext.Request.Cookies.Get("OnlineOrderPayment");
            string paymentData = cookie.Value;
            paymentData = paymentData.Replace("_filler_text_", order.Id);
            IPaymentService paymentDataService = new PaymentService();
            paymentDataService.AddPayment(paymentContext, paymentData);
            //
            order.OrderStatus = "Payment Processed";
            orderService.CreateOnlineOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);
            //
            return RedirectToAction("Thankyou", new { orderId = order.Id });
        }

        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}