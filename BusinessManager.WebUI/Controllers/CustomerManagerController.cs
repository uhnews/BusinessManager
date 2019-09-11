using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin, POSAttendant")]
    public class CustomerManagerController : Controller
    {
        IRepository<Customer> context;

        // dependency injection
        public CustomerManagerController(IRepository<Customer> customerContext)
        {
            context = customerContext;
        }

        // GET: CustomerManagerController
        public ActionResult Index()
        {
            List<Customer> customers = context.Collection().ToList();
            return View(customers);
        }

        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            else
            {
                context.Insert(customer);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Customer customer = context.Find(Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult Edit(Customer customer, string Id)
        {
            Customer customerToEdit = context.Find(Id);
            if (customerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                customerToEdit.FirstName = customer.FirstName;
                customerToEdit.LastName = customer.LastName;
                customerToEdit.Email = customer.Email;
                customerToEdit.CompanyName = customer.CompanyName;
                customerToEdit.Street = customer.Street;
                customerToEdit.City = customer.City;
                customerToEdit.State = customer.State;
                customerToEdit.ZipCode = customer.ZipCode;
                //customerToEdit.CreatedAt = customer.CreatedAt;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Customer customerToDelete = context.Find(Id);
            if (customerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customerToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Customer customerToDelete = context.Find(Id);

            if (customerToDelete == null)
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