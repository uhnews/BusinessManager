using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace BusinessManager.Core.Contracts
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetCustomers();
    }
}
