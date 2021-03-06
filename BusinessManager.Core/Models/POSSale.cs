﻿using BusinessManager.Core.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Core.Models
{
    public class POSSale : BaseEntity
    {
        public POSSale()
        {
            this.POSSaleItems = new List<POSSaleItem>();
            this.POSSalePayments = new List<Payment>();
            this.Customers = new List<CustomerViewModel>();
        }

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

        public decimal TotalAmount { get; set; }

        public int TotalItemCount { get; set; }

        public virtual ICollection<POSSaleItem> POSSaleItems { get; set; }

        public virtual ICollection<CustomerViewModel> Customers { get; set; }

        [NotMapped]
        public virtual ICollection<Payment> POSSalePayments { get; set; }
    }
}