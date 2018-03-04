using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoanRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLoanRequestServicesClient sdgenreq = new ESSWebService.SDSLoanRequestServicesClient();
                sdgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdgenreq.getAllLoanReqeust(callcont, username, compid).parmLoanRequestList
                          select new DTOLoans
                          {
                              LoanRequestId = a.LoanRequestId,
                              LoanType = a.Loantype.ToString(),
                              LoanRequested = a.Loanvalue.ToString(),
                              MonthlyInstallment = a.MonthlyInstallment.ToString(),
                              NoofInstallment = a.NoOfInsatllment.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              ReasonCode = a.ReasonCode,
                              Name = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              URL = "/Loan/ViewLoanbyRequestId?loanreqid=" + a.LoanRequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }








        public ActionResult CreateNewLoanRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public JsonResult getrequestreasons()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLoanRequestServicesClient sdseosreq = new ESSWebService.SDSLoanRequestServicesClient();
            sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdseosreq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public string CreateLoanRequest(string reason,
            string loantype, string comment,
            string loanamount, string noofinstallment,
            string deductionistartdate)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLoanRequestServicesClient sdsloanreq = new ESSWebService.SDSLoanRequestServicesClient();
                sdsloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                ESSWebService.LoanRequest _creloanreq = new ESSWebService.LoanRequest();
                sdsloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                _creloanreq.Comments = comment;
                _creloanreq.RequestDate = DateTime.Now;
                _creloanreq.WorkerRecid = username;
                _creloanreq.Loantype = loantype == "-1" ? null : loantype;
                _creloanreq.Loanvalue = Convert.ToDecimal(loanamount);
                _creloanreq.NoOfInsatllment = Convert.ToInt32(noofinstallment);
                _creloanreq.MonthlyInstallment = Convert.ToDecimal(loanamount) / Convert.ToInt32(noofinstallment);
                _creloanreq.DeductionStartDate = new DateTime(Convert.ToInt32(deductionistartdate.Split('/')[2]), Convert.ToInt32(deductionistartdate.Split('/')[1]), Convert.ToInt32(deductionistartdate.Split('/')[0]));
                _creloanreq.ReasonCode = reason == "-1" ? null : reason;
                string g = sdsloanreq.createLoanRequest(callcont, _creloanreq, compid);
                return g;
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";

            }
        }

        public ActionResult ViewLoanbyRequestId(string loanreqid)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLoanRequestServicesClient sdsloanreq = new ESSWebService.SDSLoanRequestServicesClient();
                sdsloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdsloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = sdsloanreq.getLoanReqeust(callcont, loanreqid, compid);
                ss.NameAr = Workflowforrequest(loanreqid);
                return View(ss);

            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public string ApproveELoanRequestbyLoanId(string loanid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                try
                {
                    ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                    var compid = Request.Cookies["compid"].Value;
                    var username = Convert.ToInt64(Request.Cookies["user"].Value);
                    ESSWebService.SDSLoanRequestServicesClient sdloanreq = new ESSWebService.SDSLoanRequestServicesClient();
                    sdloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                    sdloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                    ESSWebService.LoanRequest _crloanreq = new ESSWebService.LoanRequest();
                    _crloanreq = sdloanreq.getPendingWorkflowList(callcont, username).parmLoanRequestList.Where(x => x.LoanRequestId == loanid).FirstOrDefault();
                    return sdloanreq.Approve(callcont, _crloanreq.WorkflowItem, username, compid);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string RejecteLoanRequestbyLoanId(string loanid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLoanRequestServicesClient sdloanreq = new ESSWebService.SDSLoanRequestServicesClient();
                sdloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.LoanRequest _crloanreq = new ESSWebService.LoanRequest();
                _crloanreq = sdloanreq.getPendingWorkflowList(callcont, username).parmLoanRequestList.Where(x => x.LoanRequestId == loanid).FirstOrDefault();

                //sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, Comment);
                return sdloanreq.Reject(callcont, _crloanreq.WorkflowItem, username, Comment);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public ActionResult SubmittedLoanRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLoanRequestServicesClient sdloanreq = new ESSWebService.SDSLoanRequestServicesClient();
                sdloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sdloanreq.getPendingWorkflowList(callcont, username).parmLoanRequestList
                          select new DTOLoans
                          {
                              LoanRequestId = a.LoanRequestId,
                              LoanType = a.Loantype.ToString(),
                              LoanRequested = a.Loanvalue.ToString(),
                              MonthlyInstallment = a.MonthlyInstallment.ToString(),
                              NoofInstallment = a.NoOfInsatllment.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              ReasonCode = a.ReasonCode,
                              Name = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              URL = "/Loan/ViewLoanbyRequestId?loanreqid=" + a.LoanRequestId,
                          }).ToList();

                return View(ss.OrderByDescending(x => x.LoanRequestId).ToList());


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }


        public JsonResult getloantypes()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLoanRequestServicesClient sdsloanreq = new ESSWebService.SDSLoanRequestServicesClient();
            sdsloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdsloanreq.getLoanTypes(callcont, compid).parmRequestIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }

        public string Workflowforrequest(string requesid)
        {
            string workcom = "";
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLoanRequestServicesClient sdsloanreq = new ESSWebService.SDSLoanRequestServicesClient();
            sdsloanreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsloanreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            ESSWebService.SDSWorkflowApprovalWorkItemHistory workflowHistory = sdsloanreq.getWorkflowHistory(callcont, requesid, compid);
            if (workflowHistory != null)
            {
                foreach (ESSWebService.SDSWorkflowApprovalWorkItemComment wfHistory in workflowHistory.Comments)
                {
                    if (wfHistory.IsStep == 0)
                    {
                        workcom = workcom + (wfHistory.Time + " - " + wfHistory.ActionLabel + " - " + "Comments : " + wfHistory.Comment) + " \n";
                    }
                    else
                    {
                        workcom = workcom + (wfHistory.StepName) + " \n";
                    }

                }
            }
            return workcom;
        }
    }
}