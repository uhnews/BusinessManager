﻿using BusinessManager.Core.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class LayawayItem : BaseEntity, IChargeItem
    {
        [StringLength(128)]
        [Required]
        public string LayawayId { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }

        [StringLength(50)]
        [DisplayName("Product Name")]
        [Required]
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