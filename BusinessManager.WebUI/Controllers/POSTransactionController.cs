using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class POSTransactionController : Controller
    {
        IRepository<Customer> customers;
        IPOSTransactionService posTransactionService;
        IPOSSaleService posSaleService;

        public POSTransactionController(IPOSTransactionService posTransactionService, IPOSSaleService posSaleService, IRepository<Customer> customers)
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

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = new Customer();
            if (customer != null)
            {
                POSSale sale = new POSSale()
                {
                    // initialize all properties to blank (but valid) strings
                    CustomerId = "",
                    FirstName = "",
                    LastName = "",
                    Street = "",
                    City = "",
                    State = "",
                    ZipCode = "",
                    Email = ""
                };
                return View(sale);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
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
    }
}