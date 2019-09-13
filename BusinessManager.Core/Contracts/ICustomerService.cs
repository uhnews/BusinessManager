using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;

namespace BusinessManager.Core.Contracts
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetCustomers();
        Customer GetCustomer(string customerId);
    }
}
