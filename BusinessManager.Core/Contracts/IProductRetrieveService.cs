using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface IProductRetrieveService
    {
        List<Product> GetProducts();
        Product GetProduct(string upc);
        Product GetProductByUPC(string upc);
    }
}
