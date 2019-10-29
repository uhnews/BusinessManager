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
        IRepository<POSSale> possaleContext;
        IRepository<POSSaleItem> possaleItemContext;
        IRepository<OnlineOrder> onlineorderContext;
        IRepository<OnlineOrderItem> onlineorderItemContext;
        IRepository<CustomerNote> customernoteContext;

        // dependency injection
        public CustomerManagerController(
                                            IRepository<Customer> customerContext, 
                                            IRepository<Layaway> layawayContext, 
                                            IRepository<LayawayItem> layawayItemContext,
                                            IRepository<Invoice> invoiceContext,
                                            IRepository<InvoiceItem> invoiceItemContext,
                                            IRepository<Sequence> sequenceContext,
                                            IRepository<Payment> paymentContext,
                                            IRepository<POSSale> possaleContext,
                                            IRepository<POSSaleItem> possaleItemContext,
                                            IRepository<OnlineOrder> onlineorderContext,
                                            IRepository<OnlineOrderItem> onlineorderItemContext,
                                            IRepository<CustomerNote> customernoteContext
                                        )
        {
            this.customerContext = customerContext;
            this.layawayContext = layawayContext;
            this.layawayItemContext = layawayItemContext;
            this.invoiceContext = invoiceContext;
            this.invoiceItemContext = invoiceItemContext;
            this.sequenceContext = sequenceContext;
            this.paymentContext = paymentContext;
            this.possaleContext = possaleContext;
            this.possaleItemContext = possaleItemContext;
            this.onlineorderContext = onlineorderContext;
            this.onlineorderItemContext = onlineorderItemContext;
            this.customernoteContext = customernoteContext;
        }

        // GET: Customers
        public ActionResult Index()
        {
            IPOSSaleService possaleService = new POSSaleService(possaleContext, possaleItemContext);
            List<Customer> customers = customerContext.Collection().ToList();
            foreach (Customer customer in customers)
            {
                possaleService.GetPOSSales(customer);
            }
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
                // get ProductList
                IProductRetrieveService productService = new ProductRetrieveService();
                customer.ProductList = productService.GetProducts();

                // get Invoices
                // related Invoice records added by EF

                // sort Invoices and InvoicesItems
                ICollection<Invoice> invoices = customer.Invoices;
                customer.Invoices = invoices.OrderByDescending(i => i.ModifiedAt).ToList();
                foreach (Invoice invoice in customer.Invoices)
                {
                    ICollection<InvoiceItem> invoiceItems = invoice.InvoiceItems;
                    invoice.InvoiceItems = invoiceItems.OrderByDescending(i => i.ModifiedAt).ToList();
                }

                // get OnlineOrders
                // related OnlineOrder and OnlineOrderItem records added by EF

                // sort OnlineOrders and OnlineOrderItems
                ICollection<OnlineOrder> onlineorders = customer.OnlineOrders;
                customer.OnlineOrders = onlineorders.OrderByDescending(i => i.ModifiedAt).ToList();
                foreach (OnlineOrder onlineorder in customer.OnlineOrders)
                {
                    ICollection<OnlineOrderItem> onlineorderItems = onlineorder.OnlineOrderItems;
                    onlineorder.OnlineOrderItems = onlineorderItems.OrderByDescending(i => i.ModifiedAt).ToList();
                }

                // get Layaways
                // related Layaway and LayawayItem records added by EF

                // sort Layaways and LayawayItems
                ICollection<Layaway> layaways = customer.Layaways;
                customer.Layaways = layaways.OrderByDescending(i => i.ModifiedAt).ToList();
                foreach (Layaway layaway in customer.Layaways)
                {
                    ICollection<LayawayItem> layawayItems = layaway.LayawayItems;
                    layaway.LayawayItems = layawayItems.OrderByDescending(i => i.ModifiedAt).ToList();
                }

                // load (sorted) Invoice POSSales
                IPOSSaleService posSaleService = new POSSaleService(possaleContext, possaleItemContext);
                posSaleService.GetPOSSales(customer);

                // sort Invoice POSSaleItems
                ICollection<POSSale> possales = customer.POSSales;
                foreach (POSSale possale in customer.POSSales)
                {
                    ICollection<POSSaleItem> possaleItems = possale.POSSaleItems;
                    possale.POSSaleItems = possaleItems.OrderByDescending(i => i.ModifiedAt).ToList();
                }

                // sort and load Invoice Payments
                IPaymentService paymentService = new PaymentService();
                paymentService.GetPayments(customer);

                // sort CustomerNotes
                customer.CustomerNotes = customer.CustomerNotes.OrderBy(n => n.CreatedAt).ToList();
                //customer.CustomerNotes.ToList().ForEach(n => { n.NoteBody = n.NoteBody.Replace("\n", "<br>"); });

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
                IPOSSaleService possaleService = new POSSaleService(possaleContext, possaleItemContext);
                possaleService.GetPOSSales(customerToDelete);
                bool canDelete = customerToDelete.CustomerNotes.Count() == 0 
                                && customerToDelete.Layaways.Count() == 0 
                                && customerToDelete.Invoices.Count() == 0
                                && customerToDelete.OnlineOrders.Count() == 00
                                && customerToDelete.POSSales.Count() == 0;
                if (!canDelete)
                {
                    return RedirectToAction("Error");
                }

                customerContext.Delete(Id);
                customerContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // ==================================================================================
        //                                   AJAX Response Methods 
        // ==================================================================================

        //
        //       *********************** Invoice Methods ***********************
        //
        public JsonResult AddInvoice(string customerId)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object inserResult = dataService.AddInvoice(invoiceContext, sequenceContext, customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInvoice(string Id)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object deleteResult = dataService.DeleteInvoice(invoiceContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInvoice(string data)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object updateResult = dataService.UpdateInvoice(invoiceContext, data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddInvoiceItem(string data)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object addResult = dataService.AddItemToInvoice(invoiceItemContext, data);
            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInvoiceItem(string Id)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object deleteResult = dataService.RemoveItemFromInvoice(invoiceItemContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        public JsonResult UpdateInvoiceItem(string Id, string productDescription, int quantity, decimal price)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object updateResult = dataService.UpdateInvoiceItem(invoiceItemContext, Id, productDescription, quantity, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInvoiceItemQuantity(string Id, int quantity)
        {
            IInvoiceDataService dataService = new InvoiceDataService();
            object updateResult = dataService.UpdateInvoiceItemQuantity(invoiceItemContext, Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        //
        //       *********************** Layaway Methods ***********************
        //
        public JsonResult AddLayaway(string customerId)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object inserResult = dataService.AddLayaway(layawayContext, customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLayaway(string Id)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object deleteResult = dataService.DeleteLayaway(layawayContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLayaway(string data)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object updateResult = dataService.UpdateLayaway(layawayContext, data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddLayawayItem(string data)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object addResult = dataService.AddItemToLayaway(layawayItemContext, data);
            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLayawayItem(string Id)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object deleteResult = dataService.RemoveItemFromLayaway(layawayItemContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        public JsonResult UpdateLayawayItem(string Id, string productDescription, int quantity, decimal price)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object updateResult = dataService.UpdateLayawayItem(layawayItemContext, Id, productDescription, quantity, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLayawayItemQuantity(string Id, int quantity)
        {
            ILayawayDataService dataService = new LayawayDataService();
            object updateResult = dataService.UpdateLayawayItemQuantity(layawayItemContext, Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        //
        //       *********************** POSSale Methods ***********************
        //
        public JsonResult AddPOSSale(string customerId)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object inserResult = dataService.AddPOSSale(customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePOSSale(string Id)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object deleteResult = dataService.DeletePOSSale(Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePOSSale(string data)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object updateResult = dataService.UpdatePOSSale(data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPOSSaleItem(string data)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object addResult = dataService.AddItemToPOSSale(data);
            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePOSSaleItem(string Id)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object deleteResult = dataService.RemoveItemFromPOSSale(Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        public JsonResult UpdatePOSSaleItem(string Id, string productDescription, int quantity, decimal price)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object updateResult = dataService.UpdatePOSSaleItem(Id, productDescription, quantity, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePOSSaleItemQuantity(string Id, int quantity)
        {
            IPOSSaleService dataService = new POSSaleService(possaleContext, possaleItemContext);
            object updateResult = dataService.UpdatePOSSaleItemQuantity(Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        //
        //       *********************** OnlineOrder Methods ***********************
        //
        public JsonResult AddOnlineOrder(string customerId)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext);
            object inserResult = dataService.AddOnlineOrder(customerId);
            return Json(inserResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOnlineOrder(string Id)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext);
            object deleteResult = dataService.DeleteOnlineOrder(Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateOnlineOrder(string data)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext);
            object updateResult = dataService.UpdateOnlineOrder(data);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOnlineOrderItem(string data)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext, onlineorderItemContext);
            object addResult = dataService.AddItemToOnlineOrder(data);
            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOnlineOrderItem(string Id)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext, onlineorderItemContext);
            object deleteResult = dataService.RemoveItemFromOnlineOrder(Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}
        }

        public JsonResult UpdateOnlineOrderItem(string Id, string productDescription, int quantity, decimal price)
        {
            //IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext, onlineorderItemContext);
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext);
            object updateResult = dataService.UpdateOnlineOrderItem(Id, productDescription, quantity, price);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateOnlineOrderItemQuantity(string Id, int quantity)
        {
            IOnlineOrderService dataService = new OnlineOrderService(onlineorderContext, onlineorderItemContext);
            object updateResult = dataService.UpdateOnlineOrderItemQuantity(Id, quantity);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }

        //
        //       *********************** CustomerNote Methods ***********************
        //
        public JsonResult AddCustomerNote(string data)
        {
            ICustomerNoteService dataService = new CustomerNoteService();
            var addResult = dataService.AddCustomerNote(customernoteContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCustomerNote(string Id)
        {
            ICustomerNoteService dataService = new CustomerNoteService();
            object deleteResult = dataService.DeleteCustomerNote(customernoteContext, Id);
            return Json(deleteResult, JsonRequestBehavior.AllowGet);    // deleteResult: {Successful = value, Message = vlue}

        }

        //
        //       *********************** Product Methods ***********************
        //
        public JsonResult GetProduct(string Id)
        {
            IProductRetrieveService productService = new ProductRetrieveService();
            Product product = productService.GetProduct(Id);

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        //
        //       *********************** Payment Methods ***********************
        //
        public JsonResult AddPayment(string data)
        {
            IPaymentService dataService = new PaymentService();
            var addResult = dataService.AddPayment(paymentContext, data);

            return Json(addResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePayment(string data)
        {
            IPaymentService dataService = new PaymentService();
            var updateResult = dataService.UpdatePayment(paymentContext, data);

            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }
    }
}  