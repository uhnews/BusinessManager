using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface IInvoiceService
    {
        Invoice GetInvoice(string Id);

        List<Invoice> GetInvoices(string customerId);

        List<InvoiceItem> GetInvoiceItems(string invoiceId);
    }
}
