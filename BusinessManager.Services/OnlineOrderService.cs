using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManager.Services
{
    public class OnlineOrderService : IOnlineOrderService
    {
        IRepository<OnlineOrder> onlineorderContext;
        IRepository<OnlineOrderItem> onlineorderItemContext;

        public OnlineOrderService(IRepository<OnlineOrder> orderContext, IRepository<OnlineOrderItem> onlineorderItemContext = null)
        {
            this.onlineorderContext = orderContext;
            this.onlineorderItemContext = onlineorderItemContext;
        }

        public void CreateOnlineOrder(OnlineOrder baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (BasketItemViewModel item in basketItems)
            {
                baseOrder.OnlineOrderItems.Add(new OnlineOrderItem()
                {
                    OnlineOrderId = baseOrder.Id,
                    ProductId = item.ProductId,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    Quantity = item.Quantity
                });
            }

            onlineorderContext.Insert(baseOrder);
            onlineorderContext.Commit();
        }

        public List<OnlineOrder> GetOnlineOrderList()
        {
            return onlineorderContext.Collection().ToList();
        }

        public OnlineOrder GetOnlineOrder(string Id)
        {
            return onlineorderContext.Find(Id);
        }

        public void UpdateOnlineOrder(OnlineOrder updatedOrder)
        {
            onlineorderContext.Update(updatedOrder);
            onlineorderContext.Commit();
        }

        public object AddOnlineOrder(string customerId)
        {
            try
            {
                ICustomerService customerService = new CustomerService();
                Customer customer = customerService.GetCustomer(customerId);
                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }

                OnlineOrder onlineorder = new OnlineOrder()
                {
                    CustomerId = customerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Street = customer.Street,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    CompanyName = customer.CompanyName,
                    Email = customer.Email,
                    Phone = customer.Phone
                };
                onlineorderContext.Insert(onlineorder);
                onlineorderContext.Commit();

                // send response object
                return new { Successful = true, Message = "Online Order added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Online Order failed to add." };
            }
        }

        public object DeleteOnlineOrder(string Id)
        {
            try
            {
                onlineorderContext.Delete(Id);
                onlineorderContext.Commit();

                // send response object
                return new { Successful = true, Message = "Online Order deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Online Order failed to delete." };
            }
        }

        public object UpdateOnlineOrder(string data)
        {
            try
            {
                OnlineOrder onlineorder = JsonConvert.DeserializeObject<OnlineOrder>(data);

                string Id = onlineorder.Id;
                OnlineOrder retrievedOnlineOrder = onlineorderContext.Find(Id);

                if (retrievedOnlineOrder != null)
                {
                    retrievedOnlineOrder.CustomerId = onlineorder.CustomerId;
                    retrievedOnlineOrder.FirstName = onlineorder.FirstName;
                    retrievedOnlineOrder.LastName = onlineorder.LastName;
                    retrievedOnlineOrder.Street = onlineorder.Street;
                    retrievedOnlineOrder.City = onlineorder.City;
                    retrievedOnlineOrder.State = onlineorder.State;
                    retrievedOnlineOrder.ZipCode = onlineorder.ZipCode;
                    retrievedOnlineOrder.CompanyName = onlineorder.CompanyName;
                    retrievedOnlineOrder.Email = onlineorder.Email;
                    retrievedOnlineOrder.Phone = onlineorder.Phone;
                    retrievedOnlineOrder.OrderStatus = onlineorder.OrderStatus;
                }

                onlineorderContext.Update(retrievedOnlineOrder);
                onlineorderContext.Commit();

                // send response object
                return new { Successful = true, Message = "Online Order updated." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Online Order failed to update." };
            }
        }

        public object AddItemToOnlineOrder(string data)
        {
            var onlineorderItem = JsonConvert.DeserializeObject<OnlineOrderItem>(data);
            onlineorderItem.Id = Guid.NewGuid().ToString();

            try
            {
                onlineorderItemContext.Insert(onlineorderItem);
                onlineorderItemContext.Commit();

                // send response object
                return new { Successful = true, Message = "Item added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Item failed to add." };
            }
        }

        public object RemoveItemFromOnlineOrder(string Id)
        {
            OnlineOrderItem itemToDelete = onlineorderItemContext.Find(Id);
            if (itemToDelete == null)
            {
                return new { Successful = false, Message = "Item not found.", ItemId = "" };
            }
            else
            {
                onlineorderItemContext.Delete(Id);
                onlineorderItemContext.Commit();
                return new { Successful = true, Message = "Item deleted.", ItemId = Id };
            }
        }

        public object UpdateOnlineOrderItem(string Id, string productDescription, int quantity, decimal price)
        {
            OnlineOrderItem onlineorderItem = onlineorderItemContext.Find(Id);

            if (onlineorderItem != null)
            {
                try
                {
                    decimal oldPrice = onlineorderItem.Price;
                    onlineorderItem.ProductDescription = productDescription;
                    onlineorderItem.Quantity = quantity;
                    onlineorderItem.Price = price;
                    onlineorderItem.ModifiedAt = DateTime.Now;
                    onlineorderItemContext.Update(onlineorderItem);
                    onlineorderItemContext.Commit();
                    return new { Successful = true, Message = "Item update succeeded.", OldValue = oldPrice };
                }
                catch (Exception ex)
                {
                    // log error
                    Console.WriteLine(ex);

                    // send message
                    return new { Successful = false, Message = "Item update failed." };
                }
            }
            else
            {
                return new { Successful = false, Message = "Item not found." };
            }
        }

        public object UpdateOnlineOrderItemQuantity(string Id, int quantity)
        {
            OnlineOrderItem onlineorderItem = onlineorderItemContext.Find(Id);

            if (onlineorderItem != null)
            {
                try
                {
                    onlineorderItem.ModifiedAt = DateTime.Now;
                    onlineorderItem.Quantity = quantity;
                    onlineorderItemContext.Update(onlineorderItem);
                    onlineorderItemContext.Commit();
                    return new { Successful = true, Message = "Item update succeeded." };
                }
                catch (Exception ex)
                {
                    // log error
                    Console.WriteLine(ex);

                    // send message
                    return (new { Successful = false, Message = "Item update failed." });
                }
            }
            else
            {
                return new { Successful = false, Message = "Item not found." };
            }
        }
    }
}
