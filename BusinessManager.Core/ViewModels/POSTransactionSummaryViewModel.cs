namespace BusinessManager.Core.ViewModels
{
    public class POSTransactionSummaryViewModel
    {
        public int TransactionCount { get; set; }
        public decimal TransactionTotal { get; set; }

        public POSTransactionSummaryViewModel()
        {

        }

        public POSTransactionSummaryViewModel(int basketCount, decimal basketTotal)
        {
            this.TransactionCount = basketCount;
            this.TransactionTotal = basketTotal;
        }
    }
}
