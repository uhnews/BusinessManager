using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Linq;
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
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                POSSale sale = new POSSale()
                {
                    CustomerId = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Street = customer.Street,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Email = customer.Email
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
            sale.Email = User.Identity.Name;

            // process payment here



            //
            posSaleService.CreatePOSSale(sale, transactionItems);
            posTransactionService.ClearPOSTransaction(this.HttpContext);

            //
            return RedirectToAction("Thankyou", new { saleId = sale.Id });
        }

        public ActionResult ThankYou(string saleId)
        {
            ViewBag.SaleId = saleId;
            return View();
        }
    }
}