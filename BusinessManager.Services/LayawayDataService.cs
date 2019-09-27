using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public object AddCustomerLayaway(IRepository<Layaway> layawayContext, string customerId)
        {
            try
            {
                Layaway layaway = new Layaway()
                {
                    CustomerId = customerId,
                    AgreementDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(90)
                };
                layawayContext.Insert(layaway);
                layawayContext.Commit();

                // send response object
                return new { Successful = true, Message = "Layaway added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Layaway failed to add." };
            }
        }

        public object DeleteLayaway(IRepository<Layaway> layawayContext, string Id)
        {
            try
            {
                layawayContext.Delete(Id);
                layawayContext.Commit();

                // send response object
                return new { Successful = true, Message = "Layaway deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Layaway failed to delete." };
            }
        }
        public object UpdateLayaway(IRepository<Layaway> layawayContext, string data)
        {
            try
            {
                Layaway layaway = JsonConvert.DeserializeObject<Layaway>(data);

                string Id = layaway.Id;
                Layaway retrievedLayaway = layawayContext.Find(Id);

                if (retrievedLayaway != null)
                {
                    retrievedLayaway.CustomerId = layaway.CustomerId;
                    retrievedLayaway.AgreementDate = layaway.AgreementDate;
                    retrievedLayaway.DueDate = layaway.DueDate;
                    retrievedLayaway.DownPayment = layaway.DownPayment;
                    retrievedLayaway.ServiceFee = layaway.ServiceFee;
                    retrievedLayaway.CancellationFee = layaway.CancellationFee;
                    retrievedLayaway.OrderStatus = layaway.OrderStatus;
                }

                layawayContext.Update(retrievedLayaway);
                layawayContext.Commit();

                // send response object
                return new { Successful = true, Message = "Layaway updated." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Item failed to update." };
            }
        }

        public object AddItemToLayaway(IRepository<LayawayItem> layawayItemContext, string data)
        {
            var layawayItem = JsonConvert.DeserializeObject<LayawayItem>(data);
            layawayItem.Id = Guid.NewGuid().ToString();

            try
            {
                layawayItemContext.Insert(layawayItem);
                layawayItemContext.Commit();

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

        public object RemoveItemFromLayaway(IRepository<LayawayItem> layawayItemContext, string Id)
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

        public object UpdateLayawayItemPrice(IRepository<LayawayItem> layawayItemContext, string Id, decimal price)
        {
            LayawayItem layawayItem = layawayItemContext.Find(Id);

            if (layawayItem != null)
            {
                try
                {
                    layawayItem.Price = price;
                    layawayItem.ModifiedAt = DateTime.Now;
                    layawayItemContext.Update(layawayItem);
                    layawayItemContext.Commit();
                    return new { Successful = true, Message = "Item update succeeded." };
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

        public object UpdateLayawayItemQuantity(IRepository<LayawayItem> layawayItemContext, string Id, int quantity)
        {
            LayawayItem layawayItem = layawayItemContext.Find(Id);

            if (layawayItem != null)
            {
                try
                {
                    layawayItem.ModifiedAt = DateTime.Now;
                    layawayItem.Quantity = quantity;
                    layawayItemContext.Update(layawayItem);
                    layawayItemContext.Commit();
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
