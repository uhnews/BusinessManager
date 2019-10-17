using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OnlineOrderItems = new List<OnlineOrderItem>();
        }

        [StringLength(128)]
        [Required]
        public string CustomerId { get; set; }

        [StringLength(50)]
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [StringLength(65)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [DisplayName("E-Mail")]
        [Required]
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

        public decimal TotalAmount { get; set; }

        public int TotalItemCount { get; set; }

        public virtual ICollection<OnlineOrderItem> OnlineOrderItems { get; set; }
    }
}