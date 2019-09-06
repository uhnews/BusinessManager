using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessManager.Core.Models
{
    public class POSTransaction : BaseEntity
    {
        public POSTransaction()
        {
            this.POSTransactionItems = new List<POSTransactionItem>();
        }

        public string CustomerId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        [DisplayName("Order Status")]
        public string OrderStatus { get; set; }

        public virtual ICollection<POSTransactionItem> POSTransactionItems { get; set; }
    }
}
