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

            var query = from m in dbSet
                        select m;
            var products = query.ToList().Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SupplierPrice = p.SupplierPrice,
                Price = p.Price,
                WholesalePrice = p.WholesalePrice,
                Category = p.Category,
                Image = p.Image,
                Quantity = p.Quantity,
                QuantityMin = p.QuantityMin,
                IsService = p.IsService,
                UPC = p.UPC,
                ProductCode = p.ProductCode
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

            var query = from m in dbSet
                        select m;
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
                    product = new Product() { Id = "" };
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

        public Product GetProduct(string Id)
        {
            return GetProduct("Id", Id);
        }

        public Product GetProductByUPC(string upc)
        {

            return GetProduct("UPC", upc);
        }

        public bool FindUPC(string upc, string exceptId = "")
        {
            if (upc == "")
            {
                return false;
            }
                
            List<Product> products;
            DataContext dataContext = new DataContext();
            DbSet<Product> dbSet = dataContext.Set<Product>();

            var query = from m in dbSet
                        select m;
            try
            {
                if (exceptId == "")
                {
                    products = query.ToList().Where(p => p.UPC == upc).Select(c => new Product
                    {
                        UPC = c.UPC
                    }).ToList();
                }
                else
                {
                    products = query.ToList().Where(p => p.UPC == upc && p.Id != exceptId).Select(c => new Product
                    {
                        UPC = c.UPC
                    }).ToList();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    products = new List<Product>();
                }
                else
                {
                    throw ex;
                }
            }

            dataContext.Dispose();

            if (products != null)
            {
                return products.Count == 0 ? false : true;
            }
            else
            {
                throw new Exception("Null value (null) found instead of List<Product> object.");
            }
        }
        
        public bool FindProductCode(string productCode, string exceptId = "")
        {
            if (productCode == "")
            {
                return false;
            }

            List<Product> products;
            DataContext dataContext = new DataContext();
            DbSet<Product> dbSet = dataContext.Set<Product>();

            var query = from m in dbSet
                        select m;
            try
            {
                if (exceptId == "")
                {
                    products = query.ToList().Where(p => p.ProductCode == productCode).Select(c => new Product
                    {
                        ProductCode = c.ProductCode
                    }).ToList();
                }
                else
                {
                    products = query.ToList().Where(p => p.ProductCode == productCode && p.Id != exceptId).Select(c => new Product
                    {
                        ProductCode = c.ProductCode
                    }).ToList();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    products = new List<Product>();
                }
                else
                {
                    throw ex;
                }
            }

            dataContext.Dispose();

            if (products != null)
            {
                return products.Count == 0 ? false : true;
            }
            else
            {
                throw new Exception("Null value (null) found instead of List<Product> object.");
            }
        }
    }
}
