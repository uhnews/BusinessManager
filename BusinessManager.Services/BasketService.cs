using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessManager.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> productContext, IRepository<Basket> basketContext)
        {
            this.basketContext = basketContext;
            this.productContext = productContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Product product = productContext.Find(productId);
            if (product == null)
            {
                throw new Exception("Product not found in catalog.");
            }

            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(p => p.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    Quantity = 1,
                    Price = product.Price,
                    Image = product.Image,
                    ModifiedAt = DateTime.Now
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.ModifiedAt = DateTime.Now;
                if (item.ProductName == "") item.ProductName = product.Name;
                if (item.ProductDescription == "") item.ProductDescription = product.Description;
                ++item.Quantity;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(p => p.Id == itemId);

            if (item != null)
            {
                string basketId = item.BasketId;
                item.BasketId = basketId;
                if (item.ProductName == "") item.ProductName = "FakeName";
                if (item.ProductDescription == "") item.ProductDescription = "FakeDescription";
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var result = (from item in basket.BasketItems
                              join product in productContext.Collection() on item.ProductId equals product.Id
                              orderby item.ModifiedAt
                              select new BasketItemViewModel()
                              {
                                  Id = item.Id,
                                  BasketId = item.BasketId,
                                  Quantity = item.Quantity,
                                  ModifiedAt = item.ModifiedAt,
                                  ProductName = product.Name,
                                  ProductDescription = product.Description,
                                  Image = product.Image,
                                  Price = product.Price
                              }
                              ).ToList();
                return result;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                decimal? basketTotal = (from bi in basket.BasketItems
                                        join p in productContext.Collection() on bi.ProductId equals p.Id
                                        select bi.Quantity * p.Price).Sum();
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;
            }
            return model;
        }

        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            basket.BasketItems.Clear();
            basketContext.Commit();
        }
    }
}
