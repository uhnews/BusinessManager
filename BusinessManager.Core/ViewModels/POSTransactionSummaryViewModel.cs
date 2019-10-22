using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;

namespace BusinessManager.Core.ViewModels
{
    public class POSTransactionSummaryViewModel
    {
        public int TransactionCount { get; set; }
        public decimal TransactionTotal { get; set; }
        public ICollection<POSSaleItem> POSSaleItems { get; set; }

        public POSTransactionSummaryViewModel()
        {
            this.POSSaleItems = new List<POSSaleItem>();
        }

        public POSTransactionSummaryViewModel(int transactionCount, decimal transactionTotal)
        {
            this.TransactionCount = transactionCount;
            this.TransactionTotal = transactionTotal;
            this.POSSaleItems = new List<POSSaleItem>();
        }
    }
}
