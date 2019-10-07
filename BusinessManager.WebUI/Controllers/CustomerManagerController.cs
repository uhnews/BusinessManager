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
        IRepository<Invoice> invoiceContext;
        IRepository<InvoiceItem> invoiceItemContext;
        IRepository<Sequence> sequenceContext;
        IRepository<Payment> paymentContext;

        // dependency injection
        public CustomerManagerController(
                                            IRepository<Customer> customerContext, 
                                            IRepository<Layaway> layawayContext, 
                                            IRepository<LayawayItem> layawayItemContext,
                                            IRepository<Invoice> invoiceContext,
                                            IRepository<InvoiceItem> invoiceItemContext,
                                            IRepository<Sequence> sequenceContext,
                                            IRepository<Payment> paymentContext
                                        )
        {
            this.customerContext = customerContext;
            this.layawayContext = layawayContext;
            this.layawayItemContext = layawayItemContext;
            this.invoiceContext = invoiceContext;
            this.invoiceItemContext = invoiceItemContext;
            this.sequenceContext = sequenceContext;
            this.paymentContext = paymentContext;
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

                // sort InvoicesItems
                ICollection<Invoice> invoices = customer.Invoices;
                customer.Invoices = invoices.OrderByDescending(i => i.ModifiedAt).ToList();
                foreach (Invoice invoice in customer.Invoices)
                {
                    ICollection<InvoiceItem> invoiceItems = invoice.InvoiceItems;
                    invoice.InvoiceItems = invoiceItems.OrderByDescending(i => i.ModifiedAt).ToList();
                }

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
                customer.Layaways = layaways.OrderByDescending(i => i.ModifiedAt).ToList();
                foreach (Layaway layaway in customer.Layaways)
                {
                    ICollection<LayawayItem> layawayItems = layaway.LayawayItems;
                    layaway.LayawayItems = layawayItems.OrderByDescending(i => i.ModifiedAt).ToList();
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

        public JsonResult AddInvoice(string customerId)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            var inserResult = dataService.AddInvoice(invoiceContext, sequenceContext, customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInvoice(string Id)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            var deleteResult = dataService.DeleteInvoice(invoiceContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInvoice(string data)
        {
            IInvoiceDataService dataService = new InvoiceDataService();

            var updateResult = dataService.UpdateInvoice(invoiceContext, data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        // GET: /CustomerManager/DeleteLayawayItem
        public JsonResult AddInvoiceItem(string data)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            var addResult = dataService.AddItemToInvoice(invoiceItemContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        // GET: /CustomerManager/DeleteLayawayItem
        public JsonResult DeleteInvoiceItem(string Id)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            var deleteResult = dataService.RemoveItemFromInvoice(invoiceItemContext, Id);

            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        public JsonResult UpdateInvoiceItemPrice(string Id, decimal price)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object updateResult = dataService.UpdateInvoiceItemPrice(invoiceItemContext, Id, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInvoiceItemQuantity(string Id, int quantity)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object updateResult = dataService.UpdateInvoiceItemQuantity(invoiceItemContext, Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddLayaway(string customerId)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var inserResult = dataService.AddLayaway(layawayContext, customerId);
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
        public JsonResult AddLayawayItem(string data)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var addResult = dataService.AddItemToLayaway(layawayItemContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        // GET: /CustomerManager/DeleteLayawayItem
        public JsonResult DeleteLayawayItem(string Id)
        {
            ILayawayDataService dataService = new LayawayDataService();
            var deleteResult = dataService.RemoveItemFromLayaway(layawayItemContext, Id);

            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
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

        public JsonResult GetProduct(string Id)
        {
            IProductRetrieveService productService = new ProductRetrieveService();
            Product product = productService.GetProduct(Id);

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        // GET: /CustomerManager/AddPayment
        public JsonResult AddPayment(string data)
        {
            IPaymentService dataService = new PaymentService();
            var addResult = dataService.AddPayment(paymentContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }
    }
}  