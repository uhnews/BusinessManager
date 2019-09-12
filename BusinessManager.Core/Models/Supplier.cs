using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Supplier : BaseEntity
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

        [StringLength(50)]
        [Required]
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
        public string Phone { get; set; }

        [StringLength(65)]
        public string Website { get; set; }

        [StringLength(200)]
        public string BusinessDescription { get; set; }

        public string BusinessCategory { get; set; }
    }
}
