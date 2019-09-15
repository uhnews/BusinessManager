using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LayawayManagerController : Controller
    {
        readonly IRepository<Layaway> layawayContext;
        readonly ProductRetrieveService productRetrieveService = new ProductRetrieveService();

        // dependency injection
        public LayawayManagerController(IRepository<Layaway> layawayContext)
        {
            this.layawayContext = layawayContext;
        }

        // GET: /LayawayManager/Index
        public ActionResult Index()
        {
            List<Layaway> layaways = layawayContext.Collection().ToList();
            return View(layaways);
        }
        
        // GET: /LayawayManager/Create Page
        public ActionResult Create()
        {
            Layaway layaway = new Layaway();
            return View(layaway);
        }

        // POST: /LayawayManager/Create Page
        [HttpPost]
        public ActionResult Create(Layaway layaway, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(layaway);
            }
            else
            {
                layawayContext.Insert(layaway);
                layawayContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /LayawayManager/Edit Page
        public ActionResult Edit(string Id)
        {
            Layaway layaway = layawayContext.Find(Id);
            if (layaway == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(layaway);
            }
        }

        // POST: /LayawayManager/Edit Page
        [HttpPost]
        public ActionResult Edit(Layaway layaway, string Id, HttpPostedFileBase file)
        {
            Layaway layawayToEdit = layawayContext.Find(Id);
            if (layawayToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(layaway);
                }
                                                                                
                layawayToEdit.CustomerId = layaway.CustomerId;
                layawayToEdit.AgreementDate = layaway.AgreementDate;
                layawayToEdit.DueDate = layaway.DueDate;
                layawayToEdit.DownPayment = layaway.DownPayment;
                layawayToEdit.ServiceFee = layaway.ServiceFee;
                layawayToEdit.CancellationFee = layaway.CancellationFee;
                layawayToEdit.OrderStatus = layaway.OrderStatus;
                layawayToEdit.CreatedAt = layaway.CreatedAt;

                layawayContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /LayawayManager/Delete Page
        public ActionResult Delete(string Id)
        {
            Layaway layawayToDelete = layawayContext.Find(Id);

            if (layawayToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(layawayToDelete);
            }
        }

        // POST: /LayawayManager/Delete Page
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Layaway layawayToDelete = layawayContext.Find(Id);

            if (layawayToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                layawayContext.Delete(Id);
                layawayContext.Commit();
                return RedirectToAction("Index");
            }
        }

        //public ActionResult RemoveFromLayaway(string Id)
        //{
        //    layawayService.RemoveFromLayaway(this.HttpContext, Id);
        //    return RedirectToAction("Index");
        //}

        //public PartialViewResult LayawaySummary()
        //{
        //    var layawaySummary = layawayService.GetLayawaySummary(this.HttpContext);
        //    return PartialView(layawaySummary);
        //}
    }
}