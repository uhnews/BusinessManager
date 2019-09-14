﻿using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class LayawayItem : BaseEntity
    {
        [StringLength(128)]
        public string LayawayId { get; set; }

        [StringLength(128)]
        public string ProductId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [Range(0, 99999999999.99, ErrorMessage = "Value cannot be negative.")]
        [Required]
        public decimal Price { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        public int Quantity { get; set; }
    }
}