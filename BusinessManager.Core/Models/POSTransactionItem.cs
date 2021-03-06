﻿using BusinessManager.Core.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class POSTransactionItem : BaseEntity, IChargeItem
    {
        [StringLength(128)]
        public string POSTransactionId { get; set; }

        [StringLength(128)]
        public string ProductId { get; set; }

        [StringLength(60)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [StringLength(70)]
        [DisplayName("Description")]
        [Required]
        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int Quantity { get; set; }
    }
}
