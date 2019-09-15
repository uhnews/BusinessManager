using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class BasketItem : BaseEntity
    {
        [StringLength(128)]
        [Required]
        public string BasketId { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [DisplayName("Modified At")]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
