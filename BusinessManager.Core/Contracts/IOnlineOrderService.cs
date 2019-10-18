using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;

namespace BusinessManager.Core.Contracts
{
    public interface IOnlineOrderService
    {
        void CreateOnlineOrder(OnlineOrder order, List<BasketItemViewModel> basketItems);
        List<OnlineOrder> GetOnlineOrderList();
        OnlineOrder GetOnlineOrder(string Id);
        void UpdateOnlineOrder(OnlineOrder updatedOrder);
    }
}