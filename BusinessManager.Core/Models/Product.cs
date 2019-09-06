using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(60)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Supplier Price")]
        public decimal SupplierPrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        [DisplayName("Wholesale")]
        public decimal WholesalePrice { get; set; }

        [Required]
        public string Category { get; set; }

        public string Image { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        [DisplayName("Minimum")]
        public int QuantityMin { get; set; }

        [DefaultValue(false)]
        [DisplayName("Service")]
        public bool IsService { get; set; }

        [StringLength(20)]
        public string UPC { get; set; }         // Universal Product Code

        [DisplayName("Product Code")]
        [StringLength(20)]
        public string ProductCode { get; set; }
    }
}