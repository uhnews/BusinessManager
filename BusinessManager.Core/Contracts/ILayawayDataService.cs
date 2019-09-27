using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface ILayawayDataService
    {
        List<Layaway> GetLayaways(string customerId);
        object RemoveItemFromLayaway(IRepository<LayawayItem> layawayItemContext, string Id);
        object AddItemToLayaway(IRepository<LayawayItem> layawayItemContext, string data);
        object AddCustomerLayaway(IRepository<Layaway> layawayContext, string customerId);
        object UpdateLayawayItemPrice(IRepository<LayawayItem> layawayItemContext, string Id, decimal price);
        object UpdateLayawayItemQuantity(IRepository<LayawayItem> layawayItemContext, string Id, int quantity);
        object DeleteLayaway(IRepository<Layaway> layawayContext, string Id);
        object UpdateLayaway(IRepository<Layaway> layawayContext, string data);
    }
}
