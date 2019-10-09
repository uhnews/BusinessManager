using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class PaymentService : IPaymentService
    {
        public object AddPayment(IRepository<Payment> paymentContext, string data)
        {
            {
                var payment = JsonConvert.DeserializeObject<Payment>(data);
                payment.Id = Guid.NewGuid().ToString();

                try
                {
                    paymentContext.Insert(payment);
                    paymentContext.Commit();

                    // send response object
                    return new { Successful = true, Message = "Payment added." };
                }
                catch (Exception ex)
                {
                    // log error;
                    Console.WriteLine(ex);

                    // send response object error
                    return new { Successful = false, Message = "Payment failed to add." };
                }
            }
        }

        public void GetPayments(Customer customer)
        {
            foreach(Invoice invoice in customer.Invoices)
            {
                this.GetPayments(invoice, "Invoice");
            }

            //foreach (Layaway layaway in customer.Layaways)
            //{
            //    this.GetPayments(layaway, "Layaway");
            //}

            return;
        }

        public void GetPayments(Object model, string modelName)
        {
            string modelId = "";
            modelName = modelName.ToLower();
            switch (modelName)
            {
                case "invoice":
                    model = (Invoice)model;
                    modelId = ((Invoice)model).Id;
                    break;
                case "layaway":
                    model = (Layaway)model;
                    modelId = ((Layaway)model).Id;
                    break;
                default:
                    return;
            }
            List<Payment> payments = new List<Payment>();
            DataContext dataContext = new DataContext();
            DbSet<Payment> dbSet = dataContext.Set<Payment>();

            payments = dataContext.Payments
                       .Where(p => p.ReceivableSource.ToLower() == modelName && p.ReceivableSourceId == modelId)
                       .OrderByDescending(p => p.ModifiedAt)
                       .ToList();
            dataContext.Dispose();

            if (payments != null)
            {
                switch (modelName)
                {
                    case "invoice":
                        ((Invoice)model).InvoicePayments = payments;
                        break;
                    case "layaway":
                        ((Layaway)model).LayawayPayments = payments;
                        break;
                }                
            }

            return;
        }
    }
}
