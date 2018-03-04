using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class GeneralRequestController : Controller
    {
        // GET: GeneralRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneralRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSGeneralRequestServiceClient sdgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdgenreq.getAllGeneralRequestListByNature(callcont, username, ESSWebService.SDSGeneralNature.General, compid).parmGeneralRequestList
                          select new DTOGeneralRequests
                          {
                              TransactionId = a.TransactionId,
                              RequestId = a.RequestId,
                              RequestgroupId = "",
                              Transactiondate = a.Transactiondate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              ReqNature = a.Generalnature.ToString(),
                              ReasonCode = a.ReasonCode,
                              Name = a.NameEn.Split('-')[1],
                              State = a.Status.ToString(),
                              URL = "/GeneralRequest/ViewGeneralRequestbyId?genreqid=" + a.TransactionId,
                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult LetterRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSGeneralRequestServiceClient sdgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdgenreq.getAllGeneralRequestListByNature(callcont, username, ESSWebService.SDSGeneralNature.Letter, compid).parmGeneralRequestList
                          select new DTOGeneralRequests
                          {
                              TransactionId = a.TransactionId,
                              RequestId = a.RequestId,
                              RequestgroupId = "",
                              Transactiondate = a.Transactiondate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              ReqNature = a.Generalnature.ToString(),
                              ReasonCode = a.ReasonCode,
                              Name = a.NameEn.Split('-')[1],
                              State = a.Status.ToString(),
                              URL = "/GeneralRequest/ViewGeneralRequestbyId?genreqid=" + a.TransactionId,
                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult CreateNewGeneralRequest()
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

        public ActionResult CreateNewLetterRequest()
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
        public ActionResult ViewGeneralRequestbyId(string genreqid)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSGeneralRequestServiceClient sdsgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdsgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdsgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = sdsgenreq.getGeneralRequest(callcont, genreqid, compid);
                ss.NameAr = Workflowforrequest(genreqid);
                return View(ss);

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
            ESSWebService.SDSGeneralRequestServiceClient sdsgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
            sdsgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdsgenreq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getrequestids()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSGeneralRequestServiceClient sdsgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
            sdsgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdsgenreq.getRequestIds(callcont, compid).parmRequestIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public string CreateGeneralRequest(string reason, string requestid, string comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSGeneralRequestServiceClient sdsgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdsgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                ESSWebService.GeneralRequest _cregenreq = new ESSWebService.GeneralRequest();
                sdsgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                _cregenreq.Comments = comment;
                _cregenreq.Transactiondate = DateTime.Now;
                _cregenreq.Workerrecid = username;
                _cregenreq.RequestId = requestid;
                _cregenreq.ReasonCode = reason;
                _cregenreq.Generalnature = ESSWebService.SDSGeneralNature.General;
                string g = sdsgenreq.createGeneralRequestByNature(callcont, _cregenreq, ESSWebService.SDSGeneralNature.General, compid);
                return g;
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";

            }
        }

        public string CreateLetterRequest(string reason, string requestid, string comment, string reportlang, string sendto)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSGeneralRequestServiceClient sdsgenreq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdsgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                ESSWebService.GeneralRequest _cregenreq = new ESSWebService.GeneralRequest();
                sdsgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                _cregenreq.Comments = comment;
                _cregenreq.Transactiondate = DateTime.Now;
                _cregenreq.Workerrecid = username;
                _cregenreq.RequestId = requestid;
                _cregenreq.ReasonCode = reason;
                _cregenreq.Sendto = sendto;
                _cregenreq.Reportlanguage = (ESSPortal.ESSWebService.SDSReportLang)Convert.ToInt32(reportlang);
                _cregenreq.Generalnature = ESSWebService.SDSGeneralNature.Letter;
                //_cregenreq.ge
                string g = sdsgenreq.createGeneralRequestByNature(callcont, _cregenreq, ESSWebService.SDSGeneralNature.Letter, compid);
                return g;
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";

            }
        }

        public ActionResult SubmittedGeneralRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSGeneralRequestServiceClient sd = new ESSWebService.SDSGeneralRequestServiceClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sd.getPendingWorkflowList(callcont, username).parmGeneralRequestList
                          select new DTOGeneralRequests
                          {
                              TransactionId = a.TransactionId,
                              RequestId = a.RequestId,
                              RequestgroupId = "",
                              Transactiondate = a.Transactiondate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              ReasonCode = a.ReasonCode,
                              Name = a.NameEn.Split('-')[1],
                              State = a.Status.ToString(),
                              URL = "/GeneralRequest/ViewGeneralRequestbyId?genreqid=" + a.TransactionId,
                          }).ToList();

                return View(ss.OrderByDescending(x=>x.RequestId).ToList());


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }

        public string ApproveGeneralRequestbyRequestNumber(string leaverequest, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSGeneralRequestServiceClient sdleavereq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.GeneralRequest _crleavereq = new ESSWebService.GeneralRequest();
                // sdleavereq.getleaveRequest(callcont, leaverequest, compid);
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmGeneralRequestList.Where(x => x.TransactionId == leaverequest).FirstOrDefault();
                //  _crleavereq.Approved = ESSWebService.NoYes.Yes;
                //  sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, compid);
                return sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, compid);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string RejecteGeneralRequestbyRequestNumber(string leaverequest, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSGeneralRequestServiceClient sdleavereq = new ESSWebService.SDSGeneralRequestServiceClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.GeneralRequest _crleavereq = new ESSWebService.GeneralRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmGeneralRequestList.Where(x => x.TransactionId == leaverequest).FirstOrDefault();

                //sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, Comment);
                return sdleavereq.Reject(callcont, _crleavereq.WorkflowItem, username, Comment);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string Workflowforrequest(string requesid)
        {
            string workcom = "";
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSGeneralRequestServiceClient sdsworkcom = new ESSWebService.SDSGeneralRequestServiceClient();
            sdsworkcom.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsworkcom.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            ESSWebService.SDSWorkflowApprovalWorkItemHistory workflowHistory = sdsworkcom.getWorkflowHistory(callcont, requesid);
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