using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using BusinessManager.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class POSTransactionController : Controller
    {
        readonly List<CustomerViewModel> customers;
        readonly IPOSTransactionService posTransactionService;
        readonly IPOSSaleService posSaleService;
        readonly ICustomerService customerService = new CustomerService();
        readonly IProductRetrieveService productRetrieveService = new ProductRetrieveService();
        readonly IRepository<Payment> paymentContext;

        public POSTransactionController(
                                            IPOSTransactionService posTransactionService, 
                                            IPOSSaleService posSaleService, 
                                            List<CustomerViewModel> customers, 
                                            IRepository<Payment> paymentContext
                                       )
        {
            this.posTransactionService = posTransactionService;
            this.posSaleService = posSaleService;
            this.customers = customers;
            this.paymentContext = paymentContext;
        }

        // GET: POSTransaction
        public ActionResult Index()
        {
            var model = posTransactionService.GetPOSTransactionItems(this.HttpContext);
            ViewBag.ItemCount = model.Count;
            return View(model);
        }

        public ActionResult AddToPOSTransaction(string Id)
        {
            posTransactionService.AddToPOSTransaction(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromPOSTransaction(string Id)
        {
            posTransactionService.RemoveFromPOSTransaction(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult POSTransactionSummary()
        {
            var posTransactionSummary = posTransactionService.GetPOSTransactionSummary(this.HttpContext);
            return PartialView(posTransactionSummary);
        }

        // GET: Transaction Checkout
        [Authorize(Roles = "Admin, POSAttendant")]
        public ActionResult Checkout()
        {
            POSTransactionSummaryViewModel summaryModel = posTransactionService.GetPOSTransactionSummary(this.HttpContext);
            POSSale sale = new POSSale();
            sale.TotalAmount = summaryModel.TransactionTotal;
            sale.TotalItemCount = summaryModel.TransactionCount;
            sale.POSSaleItems = summaryModel.POSSaleItems;

            // request customer list
            sale.Customers = customerService.GetCustomers();

            return View(sale);
        }

        // POST: Transaction Checkout
        [HttpPost]
        [Authorize(Roles = "Admin, POSAttendant")]
        public ActionResult Checkout(POSSale sale)
        {
            var transactionItems = posTransactionService.GetPOSTransactionItems(this.HttpContext);

            // process payment here
            HttpCookie cookie = this.HttpContext.Request.Cookies.Get("POSPayment");
            string paymentData = cookie.Value;
            paymentData = paymentData.Replace("_filler_text_", sale.Id);
            IPaymentService paymentDataService = new PaymentService();
            paymentDataService.AddPayment(paymentContext, paymentData);
            //Payment payment = JsonConvert.DeserializeObject<Payment>(paymentData);
            

            //
            posSaleService.CreatePOSSale(sale, transactionItems);
            posTransactionService.ClearPOSTransaction(this.HttpContext);

            //
            return RedirectToAction("Index");
        }

        public ActionResult ThankYou(string saleId)
        {
            ViewBag.SaleId = saleId;
            return View();
        }

        // GET: Customer
        public JsonResult GetCustomer(string Id)
        {
            Customer customer = customerService.GetCustomer(Id);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // GET: Product
        public JsonResult GetProduct(string barcode)
        {
            Product product = productRetrieveService.GetProductByUPC(barcode);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}