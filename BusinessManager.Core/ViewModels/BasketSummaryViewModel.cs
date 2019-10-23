using BusinessManager.Core.Models;
using System.Collections.Generic;

namespace BusinessManager.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }
        public ICollection<OnlineOrderItem> OnlineOrderItems { get; set; }

        public BasketSummaryViewModel()
        {
            this.OnlineOrderItems = new List<OnlineOrderItem>();
        }

        public BasketSummaryViewModel(int basketCount, decimal basketTotal)
        {
            this.BasketCount = basketCount;
            this.BasketTotal = basketTotal;
            this.OnlineOrderItems = new List<OnlineOrderItem>();
        }
    }
}
