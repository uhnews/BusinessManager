using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using BusinessManager.Services;
using System;

using Unity;

namespace BusinessManager.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            // SQL Repositories
            container.RegisterType<IRepository<Product>, SQLRepository<Product>>();
            container.RegisterType<IRepository<ProductCategory>, SQLRepository<ProductCategory>>();
            container.RegisterType<IRepository<Basket>, SQLRepository<Basket>>();
            container.RegisterType<IRepository<BasketItem>, SQLRepository<BasketItem>>();
            container.RegisterType<IRepository<Customer>, SQLRepository<Customer>>();
            container.RegisterType<IRepository<OnlineOrder>, SQLRepository<OnlineOrder>>();
            container.RegisterType<IRepository<OnlineOrderItem>, SQLRepository<OnlineOrderItem>>();
            container.RegisterType<IRepository<POSTransaction>, SQLRepository<POSTransaction>>();
            container.RegisterType<IRepository<POSTransactionItem>, SQLRepository<POSTransactionItem>>();
            container.RegisterType<IRepository<POSSale>, SQLRepository<POSSale>>();
            container.RegisterType<IRepository<POSSaleItem>, SQLRepository<POSSaleItem>>();
            container.RegisterType<IRepository<Supplier>, SQLRepository<Supplier>>();
            container.RegisterType<IRepository<Invoice>, SQLRepository<Invoice>>();
            container.RegisterType<IRepository<InvoiceItem>, SQLRepository<InvoiceItem>>();
            container.RegisterType<IRepository<Layaway>, SQLRepository<Layaway>>();
            container.RegisterType<IRepository<LayawayItem>, SQLRepository<LayawayItem>>();
            container.RegisterType<IRepository<Payment>, SQLRepository<Payment>>();
            container.RegisterType<IRepository<CustomerNote>, SQLRepository<CustomerNote>>();
            container.RegisterType<IRepository<Attachment>, SQLRepository<Attachment>>();
            container.RegisterType<IRepository<Sequence>, SQLRepository<Sequence>>();

            // Data Service Contracts
            container.RegisterType<IBasketService, BasketService>();
            container.RegisterType<IOnlineOrderService, OnlineOrderService>();
            container.RegisterType<IPOSTransactionService, POSTransactionService>();
            container.RegisterType<IPOSSaleService, POSSaleService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<ILayawayService, LayawayService>();
        }
    }
}