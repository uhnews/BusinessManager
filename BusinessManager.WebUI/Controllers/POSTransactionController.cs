using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class POSTransactionController : Controller
    {
        IRepository<Customer> customers;
        IPOSTransactionService POSTransactionService;
        IPOSSaleService POSSaleService;

        // GET: POSTransaction
        public ActionResult Index()
        {
            return View();
        }
    }
}