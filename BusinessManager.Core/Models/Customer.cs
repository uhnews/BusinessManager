using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Core.Models
{
    public class Customer : BaseEntity
    {
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(65)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

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
        [DisplayName("Telephone")]
        public string Phone { get; set; }

        [StringLength(50)]
        [DisplayName("Telephone (2)")]
        public string Phone2 { get; set; }

        [StringLength(65)]
        public string Website { get; set; }

        public virtual ICollection<Layaway> Layaways { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<Order> OnlineOrders { get; set; }

        [NotMapped]
        public virtual ICollection<Product> ProductList { get; set; }

        public Customer()
        {
            this.Layaways = new List<Layaway>();
            this.Invoices = new List<Invoice>();
            this.OnlineOrders = new List<Order>();
            this.ProductList = new List<Product>();
        }
    }
}