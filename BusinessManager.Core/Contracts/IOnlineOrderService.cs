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
        object UpdateOnlineOrder(string data);
        object AddOnlineOrder(string customerId);
        object DeleteOnlineOrder(string Id);
        object AddItemToOnlineOrder(string data);
        object RemoveItemFromOnlineOrder(string Id);
        object UpdateOnlineOrderItem(string Id, string productDescription, int quantity, decimal price);
        object UpdateOnlineOrderItemQuantity(string Id, int quantity);
    }
}