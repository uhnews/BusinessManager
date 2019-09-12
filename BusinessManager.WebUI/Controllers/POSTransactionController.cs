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
        List<CustomerViewModel> customers;
        IPOSTransactionService posTransactionService;
        IPOSSaleService posSaleService;
        readonly CustomerService customeService = new CustomerService();

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
            sale.Customers = customeService.GetCustomers();

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
            return RedirectToAction("ThankYou", new { saleId = sale.Id });
        }

        public ActionResult ThankYou(string saleId)
        {
            ViewBag.SaleId = saleId;
            return View();
        }

        // GET: Customer
        public JsonResult Customer(string Id)
        {
            Customer customer = customeService.GetCustomer(Id);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}