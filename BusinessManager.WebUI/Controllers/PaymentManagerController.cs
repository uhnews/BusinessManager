using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    public class PaymentManagerController : Controller
    {

        IRepository<Payment> paymentContext;

        // dependency injection
        public PaymentManagerController(IRepository<Payment> paymentContext)
        {
            this.paymentContext = paymentContext;
        }

        // GET: Payments
        public ActionResult Index()
        {
            List<Payment> customers = paymentContext.Collection().ToList();
            return View(customers);
        }

        public ActionResult Create()
        {
            Payment payment = new Payment();
            return View(payment);
        }

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View(payment);
            }
            else
            {
                paymentContext.Insert(payment);
                paymentContext.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Payment payment = paymentContext.Find(Id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(payment);
            }
        }

        [HttpPost]
        public ActionResult Edit(Payment payment, string Id)
        {
            Payment paymentToEdit = paymentContext.Find(Id);
            if (paymentToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(payment);
                }

                paymentToEdit.PaymentDate = payment.PaymentDate;
                paymentToEdit.Amount = payment.Amount;
                paymentToEdit.PaymentMode = payment.PaymentMode;
                paymentToEdit.CheckNo = payment.CheckNo;
                paymentToEdit.CheckImage = payment.CheckImage;
                paymentToEdit.CheckWriter = payment.CheckWriter;
                paymentToEdit.CreditCardHolder = payment.CreditCardHolder;
                paymentToEdit.CreditCardNo = payment.CreditCardNo;
                paymentToEdit.CreditCardName = payment.CreditCardName;
                paymentToEdit.ReceivableSource = payment.ReceivableSource;
                paymentToEdit.ReceivableSourceId = payment.ReceivableSourceId;

                paymentContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /PaymentManager/Delete
        public ActionResult Delete(string Id)
        {
            Payment paymentToDelete = paymentContext.Find(Id);
            if (paymentToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(paymentToDelete);
            }
        }

        // POST: /PaymentManager/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Payment paymentToDelete = paymentContext.Find(Id);

            if (paymentToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                paymentContext.Delete(Id);
                paymentContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}