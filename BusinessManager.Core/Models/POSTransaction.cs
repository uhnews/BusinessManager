﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class POSTransaction : BaseEntity
    {
        [StringLength(128)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(65)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Telephone")]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(50)]
        [DisplayName("Order Status")]
        public string OrderStatus { get; set; }

        public virtual ICollection<POSTransactionItem> POSTransactionItems { get; set; }

        public POSTransaction()
        {
            this.POSTransactionItems = new List<POSTransactionItem>();
        }
    }
}
