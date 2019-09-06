﻿using BusinessManager.Core.Models;
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
    }
}