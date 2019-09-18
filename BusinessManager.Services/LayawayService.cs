using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessManager.Services
{
    public class LayawayService : ILayawayService
    {
        IRepository<Product> productContext;
        IRepository<Layaway> layawayContext;

        public const string LayawaySessionName = "layaway";

        public LayawayService(IRepository<Product> productContext, IRepository<Layaway> layawayContext)
        {
            this.layawayContext = layawayContext;
            this.productContext = productContext;
        }

        private Layaway GetLayaway(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(LayawaySessionName);

            Layaway layaway = new Layaway();

            if (cookie != null)
            {
                string LayawayId = cookie.Value;
                if (!string.IsNullOrEmpty(LayawayId))
                {
                    layaway = layawayContext.Find(LayawayId);
                }
                else
                {
                    if (createIfNull)
                    {
                        layaway = CreateNewLayaway(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    layaway = CreateNewLayaway(httpContext);
                }
            }

            return layaway;
        }

        private Layaway CreateNewLayaway(HttpContextBase httpContext)
        {
            Layaway layaway = new Layaway();
            layawayContext.Insert(layaway);
            layawayContext.Commit();

            HttpCookie cookie = new HttpCookie(LayawaySessionName);
            cookie.Value = layaway.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return layaway;
        }

        public void AddToLayaway(HttpContextBase httpContext, string productId)
        {
            Layaway layaway = GetLayaway(httpContext, true);
            LayawayItem item = layaway.LayawayItems.FirstOrDefault(p => p.ProductId == productId);
            if (item == null)
            {
                item = new LayawayItem()
                {
                    LayawayId = layaway.Id,
                    ProductId = productId,
                    Quantity = 1,
                    ModifiedAt = DateTime.Now
                };

                layaway.LayawayItems.Add(item);
            }
            else
            {
                ++item.Quantity;
                item.ModifiedAt = DateTime.Now;
            }

            layawayContext.Commit();
        }

        public void RemoveFromLayaway(HttpContextBase httpContext, string itemId)
        {
            Layaway layaway = GetLayaway(httpContext, true);
            LayawayItem item = layaway.LayawayItems.FirstOrDefault(p => p.Id == itemId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    --item.Quantity;
                    layawayContext.Commit();
                }
                else
                {
                    layaway.LayawayItems.Remove(item);
                    layawayContext.Commit();
                }
            }
        }

        public List<LayawayItemViewModel> GetLayawayItems(HttpContextBase httpContext)
        {
            Layaway layaway = GetLayaway(httpContext, false);
            if (layaway != null)
            {
                var result = (from b in layaway.LayawayItems
                              join p in productContext.Collection() on b.ProductId equals p.Id
                              orderby b.ModifiedAt descending
                              select new LayawayItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ModifiedAt = b.ModifiedAt,
                                  ProductId = b.ProductId,
                                  ProductName = p.Name,
                                  UPC = p.UPC,
                                  ProductCode = p.ProductCode,
                                  Image = p.Image,
                                  Price = p.Price
                              }).ToList();
                return result;
            }
            else
            {
                return new List<LayawayItemViewModel>();
            }
        }

        public LayawaySummaryViewModel GetLayawaySummary(HttpContextBase httpContext)
        {
            Layaway layaway = GetLayaway(httpContext, false);
            LayawaySummaryViewModel model = new LayawaySummaryViewModel(0, 0);
            if (layaway != null)
            {
                int? layawayCount = (from item in layaway.LayawayItems
                                         select item.Quantity).Sum();
                decimal? layawayTotal = (from bi in layaway.LayawayItems
                                             join p in productContext.Collection() on bi.ProductId equals p.Id
                                             select bi.Quantity * p.Price).Sum();
                model.LayawayCount = layawayCount ?? 0;
                model.LayawayTotal = layawayTotal ?? decimal.Zero;
            }
            return model;
        }

        public void ClearLayaway(HttpContextBase httpContext)
        {
            Layaway layaway = GetLayaway(httpContext, false);
            layaway.LayawayItems.Clear();
            layawayContext.Commit();
        }
    }
}
