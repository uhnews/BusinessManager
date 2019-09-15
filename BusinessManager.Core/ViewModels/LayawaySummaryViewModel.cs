using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.ViewModels
{
    public class LayawaySummaryViewModel
    {
        public int LayawayCount { get; set; }
        public decimal LayawayTotal { get; set; }

        public LayawaySummaryViewModel()
        {

        }

        public LayawaySummaryViewModel(int layawayCount, decimal layawayTotal)
        {
            this.LayawayCount = layawayCount;
            this.LayawayTotal = layawayTotal;
        }
    }
}
