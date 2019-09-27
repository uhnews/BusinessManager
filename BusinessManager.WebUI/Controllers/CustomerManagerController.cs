using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin, POSAttendant")]
    public class CustomerManagerController : Controller
    {
        IRepository<Customer> customerContext;
        IRepository<Layaway> layawayContext;
        IRepository<LayawayItem> layawayItemContext;

        // dependency injection
        public CustomerManagerController(IRepository<Customer> customerContext, IRepository<Layaway> layawayContext, IRepository<LayawayItem> layawayItemContext)
        {
            this.customerContext = customerContext;
            this.layawayContext = layawayContext;
            this.layawayItemContext = layawayItemContext;
        }

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = customerContext.Collection().ToList();
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
                customerContext.Insert(customer);
                customerContext.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Customer customer = customerContext.Find(Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                // get Invoices
                // related Invoice records added by EF

                // get Layaways
                // related Layaway and LayawayItem records added by EF

                // get ProductList
                IProductRetrieveService productService = new ProductRetrieveService();
                customer.ProductList = productService.GetProducts();

                //LayawayDataService layawayService  = new LayawayDataService();
                //customer.Layaways = layawayService.GetLayaways(Id);

                // get (online) Orders

                // sort LayawayItems
                ICollection<Layaway> layaways = customer.Layaways;
                customer.Layaways = layaways.OrderByDescending(r => r.ModifiedAt).ToList();
                foreach (Layaway layaway in customer.Layaways)
                {
                    ICollection<LayawayItem> layawayItems = layaway.LayawayItems;
                    layaway.LayawayItems = layawayItems.OrderByDescending(r => r.ModifiedAt).ToList();
                }

                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult Edit(Customer customer, string Id)
        {
            Customer customerToEdit = customerContext.Find(Id);
            if (customerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    // reload ProductList
                    IProductRetrieveService productService = new ProductRetrieveService();
                    customer.ProductList = productService.GetProducts();

                    // reload Layaways
                    ILayawayDataService layawayService = new LayawayDataService();
                    customer.Layaways = layawayService.GetLayaways(Id);

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
                customerToEdit.Phone = customer.Phone;
                customerToEdit.Phone2 = customer.Phone2;
                customerToEdit.Website = customer.Website;

                customerContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /CustomerManager/Delete
        public ActionResult Delete(string Id)
        {
            Customer customerToDelete = customerContext.Find(Id);
            if (customerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customerToDelete);
            }
        }

        // POST: /CustomerManager/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Customer customerToDelete = customerContext.Find(Id);

            if (customerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                customerContext.Delete(Id);
                customerContext.Commit();
                return RedirectToAction("Index");
            }
        }

        public JsonResult AddLayaway(string customerId)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var inserResult = dataService.AddCustomerLayaway(layawayContext, customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLayaway(string Id)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var deleteResult = dataService.DeleteLayaway(layawayContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLayaway(string data)
        {
            ILayawayDataService dataService = new LayawayDataService();

            var updateResult = dataService.UpdateLayaway(layawayContext, data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        // GET: /CustomerManager/DeleteLayawayItem
        public JsonResult DeleteLayawayItem(string Id)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var deleteResult = dataService.RemoveItemFromLayaway(layawayItemContext, Id);

            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        // GET: /CustomerManager/DeleteLayawayItem
        public JsonResult AddLayawayItem(string data)
        {
            //var layawayItem = JsonConvert.DeserializeObject<LayawayItem>(data);
            //layawayItem.Id = Guid.NewGuid().ToString();

            ILayawayDataService dataService = new LayawayDataService();
            var addResult = dataService.AddItemToLayaway(layawayItemContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProduct(string Id)
        {
            IProductRetrieveService productService = new ProductRetrieveService();
            Product product = productService.GetProduct(Id);

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLayawayItemPrice(string Id, decimal price)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object updateResult = dataService.UpdateLayawayItemPrice(layawayItemContext, Id, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLayawayItemQuantity(string Id, int quantity)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object updateResult = dataService.UpdateLayawayItemQuantity(layawayItemContext, Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }
    }
}  