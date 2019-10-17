using BusinessManager.Core.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class OnlineOrderItem : BaseEntity, IChargeItem
    {
        [StringLength(128)]
        [Required]
        public string OrderId { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }

        [StringLength(60)]
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [StringLength(70)]
        [DisplayName("Description")]
        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}