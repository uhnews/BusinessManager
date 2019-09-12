using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class SupplierManagerController : Controller
    {

        IRepository<Supplier> context;

        // dependency injection
        public SupplierManagerController(IRepository<Supplier> supplierContext)
        {
            context = supplierContext;
        }

        // GET: Suppliers
        public ActionResult Index()
        {
            List<Supplier> suppliers = context.Collection().ToList();
            return View(suppliers);
        }

        public ActionResult Create()
        {
            Supplier Supplier = new Supplier();
            return View(Supplier);
        }

        [HttpPost]
        public ActionResult Create(Supplier Supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(Supplier);
            }
            else
            {
                context.Insert(Supplier);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Supplier Supplier = context.Find(Id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(Supplier);
            }
        }

        [HttpPost]
        public ActionResult Edit(Supplier Supplier, string Id)
        {
            Supplier supplierToEdit = context.Find(Id);
            if (supplierToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(Supplier);
                }

                supplierToEdit.FirstName = Supplier.FirstName;
                supplierToEdit.LastName = Supplier.LastName;
                supplierToEdit.Email = Supplier.Email;
                supplierToEdit.Phone = Supplier.Phone;
                supplierToEdit.CompanyName = Supplier.CompanyName;
                supplierToEdit.Street = Supplier.Street;
                supplierToEdit.City = Supplier.City;
                supplierToEdit.State = Supplier.State;
                supplierToEdit.ZipCode = Supplier.ZipCode;
                supplierToEdit.Website = Supplier.Website;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Supplier supplierToDelete = context.Find(Id);
            if (supplierToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(supplierToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Supplier supplierToDelete = context.Find(Id);

            if (supplierToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}