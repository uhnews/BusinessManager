using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface IChargeItem
    {
        [Required]
        [StringLength(128)]
        string ProductId { get; set; }

        [Required]
        [StringLength(50)]
        string ProductName { get; set; }

        [StringLength(70)]
        [DisplayName("Description")]
        [Required]
        string ProductDescription { get; set; }

        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        [Required]
        decimal Price { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        int Quantity { get; set; }

        [StringLength(200)]
        string Image { get; set; }
    }
}
