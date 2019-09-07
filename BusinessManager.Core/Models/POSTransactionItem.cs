﻿using System.ComponentModel;

namespace BusinessManager.Core.Models
{
    public class POSTransactionItem : BaseEntity
    {
        public string POSTransactionId { get; set; }

        public string ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
    }
}