using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class InvoiceService : IInvoiceService
    {
        public Invoice GetInvoice(string Id)
        {
            // implement actual code to get Invoice
            // strategy: create linq query to get sql-views of Invoice Info along with sql-views of all the Invoice Items and Payments

            return new Invoice();
        }

        public List<Invoice> GetInvoices(string customerId)
        {
            List<Invoice> invoices = new List<Invoice>();

            DataContext dataContext = new DataContext();
            DbSet<Invoice> dbSet = dataContext.Set<Invoice>();

            var query = from m in dbSet
                        select m;
            invoices = query.ToList().Where(m => m.Id == customerId).Select(i => new Invoice
            {
                Id = i.Id,
                InvoiceNo = i.InvoiceNo,
                InvoiceDate = i.InvoiceDate,
                PayerFirstName = i.PayerFirstName,
                PayerLastName = i.PayerLastName,
                PayerCompany = i.PayerCompany,
                PayerStreet = i.PayerStreet,
                PayerCity = i.PayerCity,
                PayerState = i.PayerState,
                PayerZipCode = i.PayerZipCode,
                PayerEmail = i.PayerEmail,
                PayerPhone = i.PayerPhone,
                CreatedAt = i.CreatedAt,
                InvoiceItems = new List<InvoiceItem>()
            }).ToList();

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

        public List<InvoiceItem> GetInvoiceItems(string invoiceId)
        {
            return new List<InvoiceItem>();
        }
    }
}
