using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class ProductSerialNum : BaseEntity
    {
        public string ProductId { get; set; }

        [Required]
        [DisplayName("S/N")]
        public string SerialNumber { get; set; }
    }
}
