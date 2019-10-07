using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
