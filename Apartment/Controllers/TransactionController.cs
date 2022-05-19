using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLibrary.BusinessLogic;
using Apartment.Models;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;
using DataAccessLibrary.Models;

namespace Apartment.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TransactionController : BaseController
    {

        ITenatProcessor _iTenantProcessor;
        ITransactionProcessor _iTransactionProcessor;

        public TransactionController(ITenatProcessor iTenantProcessor, ITransactionProcessor iTransactionProcessor)
        {
            _iTenantProcessor = iTenantProcessor;
            _iTransactionProcessor = iTransactionProcessor;

        }
        // GET: Transaction
        public ActionResult Index()
        {
            ViewBag.ListType = "all";
            ViewBag.TenantId = "NA";
            return View("AddEditTransactions");
        }

      

        public ActionResult AddEditTransactions(string id)
        {
            
            TenantViewModel tenantView = new TenantViewModel();

            ViewBag.ListType = "specific";
            ViewBag.TenantId = id;
            var tenant = _iTenantProcessor.getSpecificTenant(System.Convert.ToInt32(id));

            tenantView.TenantId = tenant.TenantId;

            tenantView.TenantName = tenant.TenantName;
            tenantView.TenantAddress = tenant.TenantAddress;
            tenantView.TenantPhoneNumber = tenant.TenantPhoneNumber;
            tenantView.TenantBalance = tenant.TenantBalance;



            return View(tenantView);
        }

        public ActionResult AddTransactionForm(string id)
        {
            TransactionViewModel transaction = new TransactionViewModel();
            

            transaction.TenantId = Convert.ToInt32(id);
            return PartialView(transaction);
        }

        [HttpPost]
        public ActionResult AddTransactionForm(TransactionViewModel transaction)
        {
            bool receiptNumberDoesNotExists = false;

            TransactionModel checkDuplicate = new TransactionModel();

            if (transaction.ReceiptNumber.HasValue)
            {
                checkDuplicate = _iTransactionProcessor.getTransactionByReceiptNumber(transaction.ReceiptNumber.Value);

                if (checkDuplicate == null)
                {
                    receiptNumberDoesNotExists = true;
                }
                else
                {
                    ModelState.AddModelError("ReceiptNumber", "Receipt Number already exists in the database.");
                }

            }

            if (ModelState.IsValid && receiptNumberDoesNotExists)
            {
                string strPayment = transaction.PaymentAmount.Replace(",","");
                int payment = Convert.ToInt32(strPayment);

                _iTransactionProcessor.addTransactionForTenant(transaction.TenantId, transaction.ReceiptNumber.GetValueOrDefault(), transaction.DatePosted, transaction.ReceivedBy, payment,transaction.BillPeriod);

                return Json(new
                {
                    status = "success"
                    //return values if needed
                });
            }

           
            return Json(new
            {
                status = "failure",
                formErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
            });

        }

        public ActionResult EditTransactionForm(string id)
        {
            TransactionViewModel tm = new TransactionViewModel();

            var transaction = _iTransactionProcessor.getSpecificTransactionForTenant(Convert.ToInt32(id));

            var dt = Convert.ToDateTime(transaction.DatePosted, CultureInfo.CurrentCulture).ToString("MM/dd/yyyy");
            var dtBill = Convert.ToDateTime(transaction.BillPeriod, CultureInfo.CurrentCulture).ToString("MMMM yyy");

            tm.TransactionId = transaction.TransactionId;
            tm.TenantId = transaction.TenantId;
            tm.DatePosted = dt;
            tm.ReceiptNumber = transaction.ReceiptNumber;
            tm.ReceivedBy = transaction.ReceivedBy;
            tm.PaymentAmount = transaction.PaymentAmount.ToString();
            tm.BillPeriod = dtBill;

            return PartialView(tm);
        }

        [HttpPost]
        public ActionResult EditTransactionForm(TransactionViewModel transaction)
        {
            bool receiptNumberDoesNotExists = false;

            TransactionModel checkDuplicate = new TransactionModel();

            if (transaction.ReceiptNumber.HasValue)
            {
                checkDuplicate = _iTransactionProcessor.getTransactionByReceiptNumber(transaction.ReceiptNumber.Value,transaction.TransactionId);

                if (checkDuplicate == null)
                {
                    receiptNumberDoesNotExists = true;
                }
                else
                {
                    ModelState.AddModelError("ReceiptNumber", "Receipt Number already exists in the database.");
                }

            }

            if (ModelState.IsValid && receiptNumberDoesNotExists)
            {
           

                _iTransactionProcessor.updateTransactionForTenant(transaction.TransactionId, transaction.TenantId, transaction.ReceiptNumber.GetValueOrDefault(), transaction.DatePosted, transaction.ReceivedBy, Convert.ToInt32(transaction.PaymentAmount),transaction.BillPeriod);
                return Json(new
                {
                    status = "success"
                    //return values if needed
                });
            }

            return Json(new
            {
                status = "failure",
                formErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
            });

        }

        public ActionResult DeleteTransaction(string id, string type)
        {
          

            if (ModelState.IsValid)
            {
                var tenantTransaction =_iTransactionProcessor.getSpecificTransactionForTenant(Convert.ToInt32(id));
                _iTransactionProcessor.deleteTransactionForTenant(Convert.ToInt32(id));
                if (type == "specific")
                {
                    return RedirectToAction("AddEditTransactions", "Transaction", new { id = tenantTransaction.TenantId });
                }
                else
                {
                    return RedirectToAction("Index", "Transaction");
                }
                
               
            }

            return RedirectToAction("Index", "Tenant");
        }

    }
}