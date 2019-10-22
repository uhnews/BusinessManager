using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessManager.Services
{
    public class POSTransactionService : IPOSTransactionService
    {
        IRepository<Product> productContext;
        IRepository<POSTransaction> POSTransactionContext;

        public const string POSTransactionSessionName = "posTransaction";

        public POSTransactionService(IRepository<Product> productContext, IRepository<POSTransaction> POSTransactionContext)
        {
            this.POSTransactionContext = POSTransactionContext;
            this.productContext = productContext;
        }

        private POSTransaction GetPOSTransaction(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(POSTransactionSessionName);

            POSTransaction posTransaction = new POSTransaction();

            if (cookie != null)
            {
                string POSTransactionId = cookie.Value;
                if (!string.IsNullOrEmpty(POSTransactionId))
                {
                    posTransaction = POSTransactionContext.Find(POSTransactionId);
                }
                else
                {
                    if (createIfNull)
                    {
                        posTransaction = CreateNewPOSTransaction(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    posTransaction = CreateNewPOSTransaction(httpContext);
                }
            }

            return posTransaction;
        }

        private POSTransaction CreateNewPOSTransaction(HttpContextBase httpContext)
        {
            POSTransaction posTransaction = new POSTransaction();
            POSTransactionContext.Insert(posTransaction);
            POSTransactionContext.Commit();

            HttpCookie cookie = new HttpCookie(POSTransactionSessionName);
            cookie.Value = posTransaction.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return posTransaction;
        }

        public void AddToPOSTransaction(HttpContextBase httpContext, string productId)
        {
            IProductRetrieveService productDataService = new ProductRetrieveService();
            Product product = productDataService.GetProduct(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            POSTransaction posTransaction = GetPOSTransaction(httpContext, true);
            POSTransactionItem item = posTransaction.POSTransactionItems.FirstOrDefault(p => p.ProductId == productId);
            if (item == null)
            {
                item = new POSTransactionItem()
                {
                    POSTransactionId = posTransaction.Id,
                    ProductId = productId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    Quantity = 1,
                    Price = product.Price,
                    ModifiedAt = DateTime.Now
                };

                posTransaction.POSTransactionItems.Add(item);
            }
            else
            {
                ++item.Quantity;
                item.ModifiedAt = DateTime.Now;
            }

            POSTransactionContext.Commit();
        }

        public void RemoveFromPOSTransaction(HttpContextBase httpContext, string itemId)
        {
            POSTransaction posTransaction = GetPOSTransaction(httpContext, true);
            POSTransactionItem item = posTransaction.POSTransactionItems.FirstOrDefault(p => p.Id == itemId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    --item.Quantity;
                    POSTransactionContext.Commit();
                }
                else
                {
                    posTransaction.POSTransactionItems.Remove(item);
                    POSTransactionContext.Commit();
                }
            }
        }

        public List<POSTransactionItemViewModel> GetPOSTransactionItems(HttpContextBase httpContext)
        {
            POSTransaction posTransaction = GetPOSTransaction(httpContext, false);
            if (posTransaction != null)
            {
                var result = (from b in posTransaction.POSTransactionItems
                              join p in productContext.Collection() on b.ProductId equals p.Id
                              orderby b.ModifiedAt descending
                              select new POSTransactionItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ModifiedAt = b.ModifiedAt,
                                  ProductName = p.Name,
                                  ProductDescription = p.Description,
                                  UPC = p.UPC,
                                  ProductCode = p.ProductCode,
                                  Image = p.Image,
                                  Price = p.Price
                              }).ToList();
                return result;
            }
            else
            {
                return new List<POSTransactionItemViewModel>();
            }
        }

        public POSTransactionSummaryViewModel GetPOSTransactionSummary(HttpContextBase httpContext)
        {
            POSTransaction posTransaction = GetPOSTransaction(httpContext, false);
            POSTransactionSummaryViewModel model = new POSTransactionSummaryViewModel(0, 0);
            if (posTransaction != null)
            {
                int? transactionCount = (from item in posTransaction.POSTransactionItems
                                    select item.Quantity).Sum();
                decimal? transactionTotal = (from pi in posTransaction.POSTransactionItems
                                        join p in productContext.Collection() on pi.ProductId equals p.Id
                                        select pi.Quantity * p.Price).Sum();
                model.TransactionCount = transactionCount ?? 0;
                model.TransactionTotal = transactionTotal ?? decimal.Zero;

                foreach (var item in posTransaction.POSTransactionItems)
                {
                    POSSaleItem possaleItem = new POSSaleItem()
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ProductDescription = item.ProductDescription,
                        Image = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    model.POSSaleItems.Add(possaleItem);
                }
            }
            return model;
        }

        public void ClearPOSTransaction(HttpContextBase httpContext)
        {
            POSTransaction posTransaction = GetPOSTransaction(httpContext, false);
            posTransaction.POSTransactionItems.Clear();
            POSTransactionContext.Commit();
        }
    }
}
