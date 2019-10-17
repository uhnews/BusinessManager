using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using System.Collections.Generic;

namespace BusinessManager.Core.Contracts
{
    public interface IPOSSaleService
    {
        void CreatePOSSale(POSSale posSale, List<POSTransactionItemViewModel> posSaleItems);
        List<POSSale> GetPOSSaleList();
        POSSale GetPOSSale(string Id);
        void UpdatePOSSale(POSSale updatedPOSSale);
        void GetPOSSales(Customer customer);
        object AddPOSSale(string customerId);
        object DeletePOSSale(string Id);
        object UpdatePOSSale(string data);
        object AddItemToPOSSale(string data);
        object RemoveItemFromPOSSale(string Id);
        object UpdatePOSSaleItem(string Id, string productDescription, int quantity, decimal price);
        object UpdatePOSSaleItemQuantity(string Id, int quantity);
    }
}