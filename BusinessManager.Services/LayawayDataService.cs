using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessManager.Services
{
    public class LayawayDataService : ILayawayDataService
    {
        public List<Layaway> GetLayaways(string customerId)
        {
            List<Layaway> layaways = new List<Layaway>();

            DataContext dataContext = new DataContext();
            DbSet<Layaway> dbSet = dataContext.Set<Layaway>();

            layaways = dataContext.Layaways
                       .Where(l => l.CustomerId == customerId)
                       .Include("LayawayItems").ToList();
           
            dataContext.Dispose();

            if (layaways != null)
            {
                return layaways;
            }
            else
            {
                return new List<Layaway>();
            }
        }

        public object RemoveFromLayaway(IRepository<LayawayItem> layawayItemContext, string Id)
        {
            LayawayItem itemToDelete = layawayItemContext.Find(Id);
            if (itemToDelete == null)
            {
                return new { Successful = false, Message = "Item not found.", ItemId = "" };
            }
            else
            {
                layawayItemContext.Delete(Id);
                layawayItemContext.Commit();
                return new { Successful = true, Message = "Item deleted.", ItemId = Id };
            }

        }
    }
}
