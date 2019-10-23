using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManager.Services
{
    public class POSSaleService : IPOSSaleService
    {
        IRepository<POSSale> possaleContext;
        IRepository<POSSaleItem> possaleItemContext;
        public POSSaleService(IRepository<POSSale> possaleContext, IRepository<POSSaleItem> possaleItemContext)
        {
            this.possaleContext = possaleContext;
            this.possaleItemContext = possaleItemContext;
        }

        public void CreatePOSSale(POSSale basePOSSale, List<POSTransactionItemViewModel> posTransactionItems)
        {
            foreach (POSTransactionItemViewModel item in posTransactionItems)
            {
                basePOSSale.POSSaleItems.Add(new POSSaleItem()
                {
                    POSSaleId = basePOSSale.Id,
                    ProductId = item.ProductId,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    Quantity = item.Quantity
                });
            }

            possaleContext.Insert(basePOSSale);
            possaleContext.Commit();
        }

        public List<POSSale> GetPOSSaleList()
        {
            return possaleContext.Collection().ToList();
        }

        public POSSale GetPOSSale(string Id)
        {
            return possaleContext.Find(Id);
        }

        public void UpdatePOSSale(POSSale updatedPOSSale)
        {
            possaleContext.Update(updatedPOSSale);
            possaleContext.Commit();
        }

        public void GetPOSSales(Customer customer)
        {
            List<POSSale> sales = possaleContext.Collection().Where(s => s.CustomerId == customer.Id).OrderByDescending(s => s.ModifiedAt).ToList();
            customer.POSSales = sales;
        }

        public object AddPOSSale(string customerId)
        {
            try
            {
                ICustomerService customerService = new CustomerService();
                Customer customer = customerService.GetCustomer(customerId);

                POSSale possale = new POSSale()
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
                possaleContext.Insert(possale);
                possaleContext.Commit();

                // send response object
                return new { Successful = true, Message = "POS Sale added." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "POS Sale failed to add." };
            }
        }

        public object DeletePOSSale(string Id)
        {
            try
            {
                possaleContext.Delete(Id);
                possaleContext.Commit();

                // send response object
                return new { Successful = true, Message = "POS Sale deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "POS Sale failed to delete." };
            }
        }

        public object UpdatePOSSale(string data)
        {
            try
            {
                POSSale possale = JsonConvert.DeserializeObject<POSSale>(data);

                string Id = possale.Id;
                POSSale retrievedPOSSale = possaleContext.Find(Id);

                if (retrievedPOSSale != null)
                {
                    retrievedPOSSale.CustomerId = possale.CustomerId;
                    retrievedPOSSale.FirstName = possale.FirstName;
                    retrievedPOSSale.LastName = possale.LastName;
                    retrievedPOSSale.Street = possale.Street;
                    retrievedPOSSale.City = possale.City;
                    retrievedPOSSale.State = possale.State;
                    retrievedPOSSale.ZipCode = possale.ZipCode;
                    retrievedPOSSale.CompanyName = possale.CompanyName;
                    retrievedPOSSale.Email = possale.Email;
                    retrievedPOSSale.Phone = possale.Phone;
                }

                possaleContext.Update(retrievedPOSSale);
                possaleContext.Commit();

                // send response object
                return new { Successful = true, Message = "POS Sale updated." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "POS Sale failed to update." };
            }
        }

        public object AddItemToPOSSale(string data)
        {
            var possaleItem = JsonConvert.DeserializeObject<POSSaleItem>(data);
            possaleItem.Id = Guid.NewGuid().ToString();

            try
            {
                possaleItemContext.Insert(possaleItem);
                possaleItemContext.Commit();

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

        public object RemoveItemFromPOSSale(string Id)
        {
            POSSaleItem itemToDelete = possaleItemContext.Find(Id);
            if (itemToDelete == null)
            {
                return new { Successful = false, Message = "Item '" + Id + "' not  found.", ItemId = "" };
            }
            else
            {
                possaleItemContext.Delete(Id);
                possaleItemContext.Commit();
                return new { Successful = true, Message = "Item deleted.", ItemId = Id };
            }
        }

        public object UpdatePOSSaleItem(string Id, string productDescription, int quantity, decimal price)
        {
            POSSaleItem invoiceItem = possaleItemContext.Find(Id);

            if (invoiceItem != null)
            {
                try
                {
                    decimal oldPrice = invoiceItem.Price;
                    invoiceItem.ProductDescription = productDescription;
                    invoiceItem.Quantity = quantity;
                    invoiceItem.Price = price;
                    invoiceItem.ModifiedAt = DateTime.Now;
                    possaleItemContext.Update(invoiceItem);
                    possaleItemContext.Commit();
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

        public object UpdatePOSSaleItemQuantity(string Id, int quantity)
        {
            POSSaleItem possaleItem = possaleItemContext.Find(Id);

            if (possaleItem != null)
            {
                try
                {
                    possaleItem.ModifiedAt = DateTime.Now;
                    possaleItem.Quantity = quantity;
                    possaleItemContext.Update(possaleItem);
                    possaleItemContext.Commit();
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
