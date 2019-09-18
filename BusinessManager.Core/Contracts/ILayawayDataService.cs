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
        object RemoveFromLayaway(IRepository<LayawayItem> layawayItemContext, string Id);
    }
}
