using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class BasketItem : BaseEntity
    {
        [StringLength(128)]
        public string BasketId { get; set; }

        [StringLength(128)]
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
