using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Models
{
    public abstract class ChargeItem
    {
        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(70)]
        [DisplayName("Description")]
        [Required]
        public string ProductDescription { get; set; }

        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        [Required]
        public decimal Price { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        public int Quantity { get; set; }

        [StringLength(200)]
        public string Image { get; set; }
    }
}
