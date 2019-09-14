using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Layaway : BaseEntity
    {
        [StringLength(128)]
        public string CustomerId { get; set; }

        [DisplayName("Agreement Date")]
        [Required]
        public DateTimeOffset AgreementDate { get; set; }

        [DisplayName("Due Date")]
        public DateTimeOffset DueDate { get; set; }

        [DisplayName("Down Payment")]
        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        public decimal DownPayment { get; set; }

        [DisplayName("Service Fee")]
        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        public decimal ServiceFee { get; set; }

        [DisplayName("Cancellation Fee")]
        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        public decimal CancellationFee { get; set; }
        
        public virtual ICollection<LayawayItem> LayawayItems { get; set; }

        public Layaway()
        {
            this.LayawayItems = new List<LayawayItem>();
        }
    }
}