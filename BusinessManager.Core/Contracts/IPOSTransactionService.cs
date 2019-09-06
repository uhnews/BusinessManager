using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace BusinessManager.Core.Contracts
{
    public interface IPOSTransactionService
    {
        void AddToPOSTransaction(HttpContextBase httpContext, string productId);
        void RemoveFromPOSTransaction(HttpContextBase httpContext, string itemId);
        List<POSTransactionItemViewModel> GetPOSTransactionItems(HttpContextBase httpContext);
        POSTransactionSummaryViewModel GetPOSTransactionSummary(HttpContextBase httpContext);
        void ClearPOSTransaction(HttpContextBase httpContext);
    }
}
