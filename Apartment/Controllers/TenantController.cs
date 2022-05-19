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

namespace Apartment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TenantController : BaseController
    {
        ITenatProcessor _iTenantProcessor;
        ITransactionProcessor _iTransactionProcessor;       

        public TenantController(ITenatProcessor iTenantProcessor, ITransactionProcessor iTransactionProcessor)
        {
            _iTenantProcessor = iTenantProcessor;
            _iTransactionProcessor = iTransactionProcessor;
         


        }

        // GET: Tenant
        public ActionResult Index()
        {

            ViewBag.isAdmin = isAdmin;

            return View();
        }


        public ActionResult GetListServerSide()
        {

            //Server Side parameters

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVal = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDir = Request["order[0][dir]"];

            List<TenantViewModel> listOfTenant = new List<TenantViewModel>();

            var data = _iTenantProcessor.getAllTenants();

            foreach (var row in data)
            {
                listOfTenant.Add(new TenantViewModel
                {
                    TenantId = row.TenantId,
                    TenantName = row.TenantName,
                    TenantAddress = row.TenantAddress,
                    // TenantBalance = row.TenantBalance,
                    TenantPhoneNumber = row.TenantPhoneNumber

                });
            }

            int totalRows = listOfTenant.Count;
            //search function/filter
            if (!string.IsNullOrEmpty(searchVal))
            {
                listOfTenant = listOfTenant.Where(x => x.TenantName.ToLower().Contains(searchVal.ToLower())
                || x.TenantAddress.ToLower().Contains(searchVal.ToLower())
                ).ToList<TenantViewModel>();
            }
            int totalRowsFiltered = listOfTenant.Count;
            //sorting
            listOfTenant = listOfTenant.OrderBy(sortColumnName + ' ' + sortDir).ToList<TenantViewModel>();

            //paging
            listOfTenant = listOfTenant.Skip(start).Take(length).ToList<TenantViewModel>();

            return Json(new { data = listOfTenant, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsFiltered }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddTenant()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddTenant(TenantViewModel tenant)
        {
            
            if (ModelState.IsValid)
            {
                _iTenantProcessor.addTenant(tenant.TenantName, tenant.TenantAddress, tenant.TenantPhoneNumber);

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

        public ActionResult ViewTenantDetails(string id)
        {

            TenantViewModel tenantView = new TenantViewModel();

            var tenant = _iTenantProcessor.getSpecificTenant(System.Convert.ToInt32(id));
           
            tenantView.TenantId = tenant.TenantId;
            tenantView.TenantName = tenant.TenantName;
            tenantView.TenantAddress = tenant.TenantAddress;
            tenantView.TenantPhoneNumber = tenant.TenantPhoneNumber;
            tenantView.TenantBalance = tenant.TenantBalance;
            
            return View(tenantView);
            
        }

        public ActionResult GetTransactionForTenantServerSide(string id)
        {

            //Server Side parameters

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVal = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDir = Request["order[0][dir]"];

            List<TransactionViewModel> listOfTransactions = new List<TransactionViewModel>();

            var data = _iTransactionProcessor.getAllTransactionForTenant(Convert.ToInt32(id));

            foreach (var row in data)
            {
                
               var dt = Convert.ToDateTime(row.DatePosted, CultureInfo.CurrentCulture).ToString("MM/dd/yyyy");

                var dtBillPeriod = Convert.ToDateTime(row.BillPeriod, CultureInfo.CurrentCulture).ToString("MMMM yyyy");

                listOfTransactions.Add(new TransactionViewModel
                {
                    TransactionId = row.TransactionId,
                    TenantId = row.TenantId,
                    ReceiptNumber = row.ReceiptNumber,
                    DatePosted = dt,
                    BillPeriod = dtBillPeriod,
                    ReceivedBy = row.ReceivedBy,
                    PaymentAmount = "P "+ row.PaymentAmount.ToString("#,#", CultureInfo.InvariantCulture) 

                });
            }

            int totalRows = listOfTransactions.Count;
            //search function/filter
            if (!string.IsNullOrEmpty(searchVal))
            {
                listOfTransactions = listOfTransactions.Where(x => x.ReceiptNumber.ToString().ToLower().Contains(searchVal.ToLower())
                || x.ReceivedBy.ToLower().Contains(searchVal.ToLower())
                ).ToList<TransactionViewModel>();
            }
            int totalRowsFiltered = listOfTransactions.Count;
            //sorting
            listOfTransactions = listOfTransactions.OrderBy(sortColumnName + ' ' + sortDir).ToList<TransactionViewModel>();

            //paging
            listOfTransactions = listOfTransactions.Skip(start).Take(length).ToList<TransactionViewModel>();

            return Json(new { data = listOfTransactions, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsFiltered }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllTransactionServerSide(string id)
        {
            //Server Side parameters

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVal = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDir = Request["order[0][dir]"];

            List<TransactionViewModel> listOfTransactions = new List<TransactionViewModel>();

            var data = _iTransactionProcessor.getAllTransactions();

            foreach (var row in data)
            {

                var dt = Convert.ToDateTime(row.DatePosted, CultureInfo.CurrentCulture).ToString("MM/dd/yyyy");

                var dtBillPeriod = Convert.ToDateTime(row.BillPeriod, CultureInfo.CurrentCulture).ToString("MMMM yyyy");

                listOfTransactions.Add(new TransactionViewModel
                {
                    TransactionId = row.TransactionId,
                    TenantId = row.TenantId,
                    ReceiptNumber = row.ReceiptNumber,
                    DatePosted = dt,
                    BillPeriod = dtBillPeriod,
                    ReceivedBy = row.ReceivedBy,
                    PaymentAmount = "P " + row.PaymentAmount.ToString("#,#", CultureInfo.InvariantCulture)

                });
            }

            int totalRows = listOfTransactions.Count;
            //search function/filter
            if (!string.IsNullOrEmpty(searchVal))
            {
                listOfTransactions = listOfTransactions.Where(x => x.ReceiptNumber.ToString().ToLower().Contains(searchVal.ToLower())
                || x.ReceivedBy.ToLower().Contains(searchVal.ToLower())
                ).ToList<TransactionViewModel>();
            }
            int totalRowsFiltered = listOfTransactions.Count;
            //sorting
            listOfTransactions = listOfTransactions.OrderBy(sortColumnName + ' ' + sortDir).ToList<TransactionViewModel>();

            //paging
            listOfTransactions = listOfTransactions.Skip(start).Take(length).ToList<TransactionViewModel>();

            return Json(new { data = listOfTransactions, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsFiltered }, JsonRequestBehavior.AllowGet);
        }





        public ActionResult EditTenantDetails(string id)
        {
            TenantViewModel tenantView = new TenantViewModel();

            var tenant = _iTenantProcessor.getSpecificTenant(System.Convert.ToInt32(id));

            tenantView.TenantId = tenant.TenantId;
            tenantView.TenantName = tenant.TenantName;
            tenantView.TenantAddress = tenant.TenantAddress;
            tenantView.TenantPhoneNumber = tenant.TenantPhoneNumber;
            tenantView.TenantBalance = tenant.TenantBalance;

            return View(tenantView);
        }

        [HttpPost]
        public ActionResult EditTenantDetails(TenantViewModel tenant)
        {
            if (ModelState.IsValid)
            {
                _iTenantProcessor.updateTenant(tenant.TenantId, tenant.TenantName,tenant.TenantAddress, tenant.TenantPhoneNumber);

                return RedirectToAction("Index", "Tenant");
            }


            return View(tenant);

        }

        public ActionResult DeleteTenantDetails(string id)
        {
            if (ModelState.IsValid)
            {
                _iTenantProcessor.deleteTenant(Convert.ToInt32(id));
            }

            return RedirectToAction("Index", "Tenant");
        }


    }
}