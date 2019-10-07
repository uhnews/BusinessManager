using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Core.Models
{
    public class Invoice : BaseEntity
    {
        [DisplayName("Invoice")]
        [Required]
        public int InvoiceNumber { get; set; }

        [StringLength(128)]
        [Required]
        public string CustomerId { get; set; }

        [DisplayName("Invoice Date")]
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

        [DisplayName("Street")]
        [StringLength(50)]
        public string PayerStreet { get; set; }

        [DisplayName("City")]
        [StringLength(50)]
        public string PayerCity { get; set; }

        [DisplayName("State")]
        [StringLength(50)]
        public string PayerState { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(50)]
        public string PayerZipCode { get; set; }

        [DisplayName("E-Mail")]
        [StringLength(50)]
        public string PayerEmail { get; set; }

        [DisplayName("Telephone")]
        [StringLength(50)]
        public string PayerPhone { get; set; }

        [DisplayName("Order Status")]
        [StringLength(50)]
        public string OrderStatus { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        [NotMapped]
        public virtual ICollection<Payment> InvoicePayments { get; set; }

        public Invoice()
        {
            this.InvoiceItems = new List<InvoiceItem>();
            this.InvoicePayments = new List<Payment>();
        }

    }
}