using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace BusinessManager.Core.Contracts
{
    public interface ILayawayService
    {
        void AddToLayaway(HttpContextBase httpContext, string productId);
        void RemoveFromLayaway(HttpContextBase httpContext, string itemId);
        List<LayawayItemViewModel> GetLayawayItems(HttpContextBase httpContext);
        LayawaySummaryViewModel GetLayawaySummary(HttpContextBase httpContext);
        void ClearLayaway(HttpContextBase httpContext);
    }
}
