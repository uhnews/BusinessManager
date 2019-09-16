﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Invoice : BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string InvoiceNo { get; set; }

        [StringLength(128)]
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public DateTimeOffset InvoiceDate { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        [Required]
        public string PayerFirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(50)]
        public string PayerLastName { get; set; }

        [DisplayName("Company")]
        [StringLength(50)]
        public string PayerCompany { get; set; }

        [StringLength(50)]
        public string PayerStreet { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(50)]
        public string ZipCode { get; set; }

        [DisplayName("E-Mail")]
        [StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Telephone")]
        [StringLength(50)]
        public string Phone { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public Invoice()
        {
            this.InvoiceItems = new List<InvoiceItem>();
        }
    }
}