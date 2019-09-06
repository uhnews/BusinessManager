using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessManager.Core.Models
{
    public class POSSale : BaseEntity
    {
        public POSSale()
        {
            this.POSSaleItems = new List<POSSaleItem>();
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

        public virtual ICollection<POSSaleItem> POSSaleItems { get; set; }
    }
}
