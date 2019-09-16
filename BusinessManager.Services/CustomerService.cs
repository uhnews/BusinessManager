using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using BusinessManager.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace BusinessManager.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerService()
        {
        }

        public List<CustomerViewModel> GetCustomers()
        {
            DataContext dataContext = new DataContext();
            DbSet<Customer> dbSet = dataContext.Set<Customer>();

            var query = from p in dbSet
                        select p;
            var customers = query.ToList().Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                CompanyName = c.CompanyName
            }).ToList();

            dataContext.Dispose();

            if (customers != null)
            {
                return customers;
            }
            else
            {
                return new List<CustomerViewModel>();
            }
        }

        public Customer GetCustomer(string customerId)
        {
            Customer customer = new Customer();

            DataContext dataContext = new DataContext();
            DbSet<Customer> dbSet = dataContext.Set<Customer>();

            var query = from m in dbSet
                        select m;
            customer = query.ToList().Where(m => m.Id == customerId).Select(c => new Customer
            {
                Id = "",
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                CompanyName = c.CompanyName,
                Street = c.Street,
                City = c.City,
                State = c.State,
                ZipCode = c.ZipCode,
                CreatedAt = new DateTime()
            }).Single();

            dataContext.Dispose();

            if (customer != null)
            {
                return customer;
            }
            else
            {
                return new Customer();
            }
        }

        List<Invoice> GetInvoices(string customerId)
        {
            List<Invoice> invoices = new List<Invoice>();

            DataContext dataContext = new DataContext();
            DbSet<Invoice> dbSet = dataContext.Set<Invoice>();

            var query = from m in dbSet
                        select m;
            invoices = query.ToList().Where(m => m.Id == customerId).Select(i => new Invoice
            {
                //Id = i.Id,
                //InvoiceNo = i.FirstName,
                //InvoiceDate = i.LastName,
                //PayerFirstName = i.Email,
                //PayerLastName = i.Phone,
                //PayerCompany = i.CompanyName,
                //Street = i.Street,
                //City = i.City,
                //State = i.State,
                //ZipCode = i.ZipCode,
                //CreatedAt = new DateTime()
            }).ToList();

            dataContext.Dispose();

            if (invoices != null)
            {
                return invoices;
            }
            else
            {
                return new List<Invoice>();
            }

        }

        List<Layaway> GetLayaways(string customerId)
        {
            return new List<Layaway>();
        }

        List<Order> GetOrders(string customerId)
        {
            return new List<Order>();
        }
    }
}