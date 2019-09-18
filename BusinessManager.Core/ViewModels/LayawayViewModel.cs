using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.ViewModels
{
    public class LayawayViewModel
    {
        public string Id { get; set; }

        public string customerId { get; set; }

        public DateTimeOffset AgreementDate { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public decimal DownPayment { get; set; }


    }
}
