using System;
using System.ComponentModel;

namespace BusinessManager.Core.ViewModels
{
    public class POSTransactionItemViewModel
    {
        public string Id { get; set; }

        public int Quantity { get; set; }

        public string ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        public string UPC { get; set; }

        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [DisplayName("Modified At")]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
