using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Core.ViewModels
{
    [NotMapped]
    public class CustomerViewModel
    {
        public string Id { get; set; }

        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }
    }
}
