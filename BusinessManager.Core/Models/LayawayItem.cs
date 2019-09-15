using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class LayawayItem : BaseEntity
    {
        [StringLength(128)]
        [Required]
        public string LayawayId { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductName { get; set; }

        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        [Required]
        public decimal Price { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        public int Quantity { get; set; }

        [DisplayName("Modified At")]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}