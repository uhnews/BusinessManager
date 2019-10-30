using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class POSSaleManagerController : Controller
    {
        IPOSSaleService posSaleService;

        public POSSaleManagerController(IPOSSaleService posSaleService)
        {
            this.posSaleService = posSaleService;
        }

        // GET: POSSaleManager
        public ActionResult Index()
        {
            List<POSSale> sales = posSaleService.GetPOSSaleList();
            return View(sales);
        }

        public ActionResult UpdatePOSSale(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            POSSale sale = posSaleService.GetPOSSale(Id);
            return View(sale);
        }

        [HttpPost]
        public ActionResult UpdatePOSSale(POSSale updatedPOSSale)
        {
            // NOT ALLOWING UPDATES FROM THIS ROUTE AT THIS TIME - WILL POSSIBLY CHANGE IN THE FUTURE
            //posSaleService.UpdatePOSSale(updatedPOSSale);

            return RedirectToAction("Index");
        }
    }
}