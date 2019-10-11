using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface IPaymentService
    {
        object AddPayment(IRepository<Payment> paymentContext, string data);
        object UpdatePayment(IRepository<Payment> paymentContext, string data);
        void GetPayments(Customer customer);
        void GetPayments(Object model, string modelName);
    }
}
