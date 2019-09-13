using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class ProductRetrieveService : IProductRetrieveService
    {
        public List<Product> GetProducts()
        {
            DataContext dataContext = new DataContext();
            DbSet<Product> dbSet = dataContext.Set<Product>();

            var query = from p in dbSet
                        select p;
            var products = query.ToList().Select(c => new Product
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            dataContext.Dispose();

            if (products != null)
            {
                return products;
            }
            else
            {
                return new List<Product>();
            }
        }

        private Product GetProduct(string property, string value)
        {
            Product product;
            DataContext dataContext = new DataContext();
            DbSet<Product> dbSet = dataContext.Set<Product>();

            var query = from p in dbSet
                        select p;
            try
            {
                if (property == "Id")
                {
                    product = query.ToList().Where(p => p.Id == value).Select(c => new Product
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Category = c.Category,
                        ProductCode = c.ProductCode,
                        UPC = c.UPC,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        QuantityMin = c.QuantityMin,
                        WholesalePrice = c.WholesalePrice,
                        IsService = c.IsService,
                        Image = c.Image
                    }).Single();
                }
                else if (property == "UPC")
                {
                    product = query.ToList().Where(p => p.UPC == value).Select(c => new Product
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Category = c.Category,
                        ProductCode = c.ProductCode,
                        UPC = c.UPC,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        IsService = c.IsService,
                        Image = c.Image
                    }).Single();
                }
                else
                {
                    product = new Product();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    product = new Product();
                    product.Id = "";
                }
                else
                {
                    throw ex;
                }
            }            

            dataContext.Dispose();

            if (product != null)
            {
                return product;
            }
            else
            {
                return new Product() { Id = ""};
            }
        }

        public Product GetProduct(string id)
        {
            return GetProduct("Id", id);
        }

        public Product GetProductByUPC(string upc)
        {

            return GetProduct("UPC", upc);
        }
    }
}
