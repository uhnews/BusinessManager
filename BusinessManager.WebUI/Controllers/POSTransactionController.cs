using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using BusinessManager.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class POSTransactionController : Controller
    {
        readonly List<CustomerViewModel> customers;
        readonly IPOSTransactionService posTransactionService;
        readonly IPOSSaleService posSaleService;
        readonly CustomerService customerService = new CustomerService();
        readonly ProductRetrieveService productRetrieveService = new ProductRetrieveService();

        public POSTransactionController(IPOSTransactionService posTransactionService, IPOSSaleService posSaleService, List<CustomerViewModel> customers)
        {
            this.posTransactionService = posTransactionService;
            this.posSaleService = posSaleService;
            this.customers = customers;
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

        // GET: Transaction Checkout Data
        [Authorize(Roles = "Admin, POSAttendant")]
        public ActionResult Checkout()
        {
            POSTransactionSummaryViewModel summaryModel = posTransactionService.GetPOSTransactionSummary(this.HttpContext);
            POSSale sale = new POSSale();
            sale.TotalAmount = summaryModel.TransactionTotal;
            sale.TotalItemCount = summaryModel.TransactionCount;

            // request customer list
            sale.Customers = customerService.GetCustomers();

            return View(sale);
        }

        // POST: Transaction Checkout Data
        [HttpPost]
        [Authorize(Roles = "Admin, POSAttendant")]
        public ActionResult Checkout(POSSale sale)
        {
            var transactionItems = posTransactionService.GetPOSTransactionItems(this.HttpContext);

            // process payment here



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