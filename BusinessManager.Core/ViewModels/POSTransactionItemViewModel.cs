using System.ComponentModel;

namespace BusinessManager.Core.ViewModels
{
    public class POSTransactionItemViewModel
    {
        public string Id { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
