using System.ComponentModel;

namespace BusinessManager.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }

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
    }
}