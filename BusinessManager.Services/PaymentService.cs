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
        public object UpdatePayment(IRepository<Payment> paymentContext, string data)
        {
            {
                var payment = JsonConvert.DeserializeObject<Payment>(data);
                
                // sanitize data a bit to ensure there are no inconsistencies regarding PaymentMode and to protect privacy
                if (payment.PaymentMode == "cash")
                {
                    // clear check & credit card info
                    payment.CheckNo = "";
                    payment.CheckImage = "";
                    payment.CheckWriter = "";
                    payment.CreditCardHolder = "";
                    payment.CreditCardNo = "";
                    payment.CreditCardName = "";
                    payment.CreditCardExpMonth = 0;
                    payment.CreditCardExpYear = 0;
                    payment.CreditCardCVV = "";
                }
                else if (payment.PaymentMode == "credit card")
                {
                    // clear check info
                    payment.CheckNo = "";
                    payment.CheckImage = "";
                    payment.CheckWriter = "";
                }
                else if (payment.PaymentMode == "check")
                {
                    // clear credit card info
                    payment.CreditCardHolder = "";
                    payment.CreditCardNo = "";
                    payment.CreditCardName = "";
                    payment.CreditCardExpMonth = 0;
                    payment.CreditCardExpYear = 0;
                    payment.CreditCardCVV = "";
                }

                try
                {
                    paymentContext.Update(payment);
                    paymentContext.Commit();

                    // send response object
                    return new { Successful = true, Message = "Payment update." };
                }
                catch (Exception ex)
                {
                    // log error;
                    Console.WriteLine(ex);

                    // send response object error
                    return new { Successful = false, Message = "Payment failed to update." };
                }
            }
        }

        public void GetPayments(Customer customer)
        {
            foreach(Invoice invoice in customer.Invoices)
            {
                this.GetPayments(invoice, "invoices");
            }

            foreach (Layaway layaway in customer.Layaways)
            {
                this.GetPayments(layaway, "layaways");
            }

            return;
        }

        public void GetPayments(Object model, string dataSet)
        {
            string modelId = "";
            dataSet = dataSet.ToLower();
            switch (dataSet)
            {
                case "invoices":
                    model = (Invoice)model;
                    modelId = ((Invoice)model).Id;
                    break;
                case "layaways":
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
                       .Where(p => p.ReceivableSource.ToLower() == dataSet && p.ReceivableSourceId == modelId)
                       .OrderByDescending(p => p.ModifiedAt)
                       .ToList();
            dataContext.Dispose();

            if (payments != null)
            {
                switch (dataSet)
                {
                    case "invoices":
                        ((Invoice)model).InvoicePayments = payments;
                        break;
                    case "layaways":
                        ((Layaway)model).LayawayPayments = payments;
                        break;
                }                
            }

            return;
        }
    }
}
