using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.DataAccess.SQL;
using System.Data.Entity;

namespace BusinessManager.Services
{
    public class InvoiceDataService : IInvoiceDataService
    {
        public List<Invoice> GetInvoices(string customerId)
        {
            List<Invoice> invoices = new List<Invoice>();

            DataContext dataContext = new DataContext();
            DbSet<Invoice> dbSet = dataContext.Set<Invoice>();

            invoices = dataContext.Invoices
                       .Where(l => l.CustomerId == customerId)
                       .Include("InvoiceItems").ToList();

            dataContext.Dispose();

            if (invoices != null)
            {
                return invoices;
            }
            else
            {
                return new List<Invoice>();
            }
        }

        public object AddInvoice(IRepository<Invoice> invoiceContext, IRepository<Sequence> sequenceContext, string customerId)
        {
            try
            {
                string sequenceName = "Invoice";

                int? invoiceNumber;
                ISequenceService sequenceService = new SequenceService();
                invoiceNumber = sequenceService.GetNextSequenceNumber(sequenceContext, sequenceName);

                if (invoiceNumber == null)
                {
                    // log error;
                    Console.WriteLine("Invalid sequence number.");

                    // send response object error
                    return new { Successful = false, Message = "Invoice failed to add." };
                }

                ICustomerService customerService = new CustomerService();
                Customer customer = customerService.GetCustomer(customerId);

                Invoice invoice = new Invoice()
                {
                    CustomerId = customerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNumber = (int)invoiceNumber,
                    PayerFirstName = customer.FirstName,
                    PayerLastName = customer.LastName,
                    PayerStreet = customer.Street,
                    PayerCity = customer.City,
                    PayerState = customer.State,
                    PayerZipCode = customer.ZipCode,
                    PayerCompany = customer.CompanyName,
                    PayerEmail = customer.Email
                };
                invoiceContext.Insert(invoice);
                invoiceContext.Commit();

                // send response object
                return new { Successful = true, Message = "Invoice added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Invoice failed to add." };
            }
        }

        public object DeleteInvoice(IRepository<Invoice> invoiceContext, string Id)
        {
            try
            {
                invoiceContext.Delete(Id);
                invoiceContext.Commit();

                // send response object
                return new { Successful = true, Message = "Invoice deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Invoice failed to delete." };
            }
        }

        public object UpdateInvoice(IRepository<Invoice> invoiceContext, string data)
        {
            try
            {
                Invoice invoice = JsonConvert.DeserializeObject<Invoice>(data);

                string Id = invoice.Id;
                Invoice retrievedInvoice = invoiceContext.Find(Id);

                if (retrievedInvoice != null)
                {
                    retrievedInvoice.CustomerId = invoice.CustomerId;
                    retrievedInvoice.InvoiceDate = invoice.InvoiceDate;
                    retrievedInvoice.InvoiceNumber = invoice.InvoiceNumber;
                    retrievedInvoice.PayerFirstName = invoice.PayerFirstName;
                    retrievedInvoice.PayerLastName = invoice.PayerLastName;
                    retrievedInvoice.PayerStreet = invoice.PayerStreet;
                    retrievedInvoice.PayerCity = invoice.PayerCity;
                    retrievedInvoice.PayerState = invoice.PayerState;
                    retrievedInvoice.PayerZipCode = invoice.PayerZipCode;
                    retrievedInvoice.PayerCompany = invoice.PayerCompany;
                    retrievedInvoice.PayerEmail = invoice.PayerEmail;
                    retrievedInvoice.OrderStatus = invoice.OrderStatus;
                }

                invoiceContext.Update(retrievedInvoice);
                invoiceContext.Commit();

                // send response object
                return new { Successful = true, Message = "Invoice updated." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Invoice failed to update." };
            }
        }

        public object AddItemToInvoice(IRepository<InvoiceItem> invoiceItemContext, string data)
        {
            var invoiceItem = JsonConvert.DeserializeObject<InvoiceItem>(data);
            invoiceItem.Id = Guid.NewGuid().ToString();

            try
            {
                invoiceItemContext.Insert(invoiceItem);
                invoiceItemContext.Commit();

                // send response object
                return new { Successful = true, Message = "Item added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Item failed to add." };
            }
        }

        public object RemoveItemFromInvoice(IRepository<InvoiceItem> invoiceItemContext, string Id)
        {
            InvoiceItem itemToDelete = invoiceItemContext.Find(Id);
            if (itemToDelete == null)
            {
                return new { Successful = false, Message = "Item not found.", ItemId = "" };
            }
            else
            {
                invoiceItemContext.Delete(Id);
                invoiceItemContext.Commit();
                return new { Successful = true, Message = "Item deleted.", ItemId = Id };
            }
        }

        public object UpdateInvoiceItemPrice(IRepository<InvoiceItem> invoiceItemContext, string Id, decimal price)
        {
            InvoiceItem invoiceItem = invoiceItemContext.Find(Id);

            if (invoiceItem != null)
            {
                try
                {
                    invoiceItem.Price = price;
                    invoiceItem.ModifiedAt = DateTime.Now;
                    invoiceItemContext.Update(invoiceItem);
                    invoiceItemContext.Commit();
                    return new { Successful = true, Message = "Item update succeeded." };
                }
                catch (Exception ex)
                {
                    // log error
                    Console.WriteLine(ex);

                    // send message
                    return new { Successful = false, Message = "Item update failed." };
                }
            }
            else
            {
                return new { Successful = false, Message = "Item not found." };
            }
        }

        public object UpdateInvoiceItemQuantity(IRepository<InvoiceItem> invoiceItemContext, string Id, int quantity)
        {
            InvoiceItem invoiceItem = invoiceItemContext.Find(Id);

            if (invoiceItem != null)
            {
                try
                {
                    invoiceItem.ModifiedAt = DateTime.Now;
                    invoiceItem.Quantity = quantity;
                    invoiceItemContext.Update(invoiceItem);
                    invoiceItemContext.Commit();
                    return new { Successful = true, Message = "Item update succeeded." };
                }
                catch (Exception ex)
                {
                    // log error
                    Console.WriteLine(ex);

                    // send message
                    return (new { Successful = false, Message = "Item update failed." });
                }
            }
            else
            {
                return new { Successful = false, Message = "Item not found." };
            }
        }
    }
}
