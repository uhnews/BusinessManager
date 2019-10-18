using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManager.Services
{
    public class OnlineOrderService : IOnlineOrderService
    {
        IRepository<OnlineOrder> orderContext;
        public OnlineOrderService(IRepository<OnlineOrder> orderContext)
        {
            this.orderContext = orderContext;
        }

        public void CreateOnlineOrder(OnlineOrder baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OnlineOrderItems.Add(new OnlineOrderItem()
                {
                    OrderId = baseOrder.Id,
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    Quantity = item.Quantity
                });
            }

            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }

        public List<OnlineOrder> GetOnlineOrderList()
        {
            return orderContext.Collection().ToList();
        }

        public OnlineOrder GetOnlineOrder(string Id)
        {
            return orderContext.Find(Id);
        }

        public void UpdateOnlineOrder(OnlineOrder updatedOrder)
        {
            orderContext.Update(updatedOrder);
            orderContext.Commit();
        }
    }
}
