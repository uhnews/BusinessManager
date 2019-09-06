using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManager.Services
{
    public class POSSaleService : IPOSSaleService
    {
        IRepository<POSSale> POSSaleContext;
        public POSSaleService(IRepository<POSSale> POSSaleContext)
        {
            this.POSSaleContext = POSSaleContext;
        }

        public void CreatePOSSale(POSSale basePOSSale, List<POSTransactionItemViewModel> POSTransactionItems)
        {
            foreach (var item in POSTransactionItems)
            {
                basePOSSale.POSSaleItems.Add(new POSSaleItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }

            POSSaleContext.Insert(basePOSSale);
            POSSaleContext.Commit();
        }

        public List<POSSale> GetPOSSaleList()
        {
            return POSSaleContext.Collection().ToList();
        }

        public POSSale GetPOSSale(string Id)
        {
            return POSSaleContext.Find(Id);
        }

        public void UpdatePOSSale(POSSale updatedPOSSale)
        {
            POSSaleContext.Update(updatedPOSSale);
            POSSaleContext.Commit();
        }
    }
}
