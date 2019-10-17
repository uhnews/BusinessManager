using System;
using System.ComponentModel;

namespace BusinessManager.Core.ViewModels
{
    public class BasketItemViewModel
    {
        public string Id { get; set; }

        public string BasketId { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [DisplayName("Modified At")]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
