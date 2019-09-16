using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;

namespace BusinessManager.Core.Contracts
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetCustomers();

        Customer GetCustomer(string customerId);

        //List<Invoice> GetInvoices(string customerId);

        //List<Layaway> GetLayaways(string customerId);

        //List<Order> GetOrders(string customerId);
    }
}
