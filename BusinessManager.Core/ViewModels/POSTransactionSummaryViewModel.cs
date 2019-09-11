namespace BusinessManager.Core.ViewModels
{
    public class POSTransactionSummaryViewModel
    {
        public int TransactionCount { get; set; }
        public decimal TransactionTotal { get; set; }

        public POSTransactionSummaryViewModel()
        {

        }

        public POSTransactionSummaryViewModel(int transactionCount, decimal transactionTotal)
        {
            this.TransactionCount = transactionCount;
            this.TransactionTotal = transactionTotal;
        }
    }
}
