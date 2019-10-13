using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface IInvoiceDataService
    {
        List<Invoice> GetInvoices(string customerId);
        object AddInvoice(IRepository<Invoice> invoiceContext, IRepository<Sequence> sequenceContext, string customerId);
        object DeleteInvoice(IRepository<Invoice> invoiceContext, string Id);
        object UpdateInvoice(IRepository<Invoice> invoiceContext, string data);
        object AddItemToInvoice(IRepository<InvoiceItem> invoiceItemContext, string data);
        object RemoveItemFromInvoice(IRepository<InvoiceItem> invoiceItemContext, string Id);
        object UpdateInvoiceItem(IRepository<InvoiceItem> invoiceItemContext, string Id, string productDescription, int quantity, decimal price);
        object UpdateInvoiceItemQuantity(IRepository<InvoiceItem> invoiceItemContext, string Id, int quantity);
    }
}
