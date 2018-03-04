using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class EOSController : Controller
    {
        // GET: EOS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewEOSRequestbyId(string EOSid)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSEosRequestServicesClient sdseosreq = new ESSWebService.SDSEosRequestServicesClient();
                sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = sdseosreq.getEosReqeust(callcont, EOSid, compid);
                ss.NameAr = Workflowforrequest(EOSid);
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult EOSRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSEosRequestServicesClient sdgenreq = new ESSWebService.SDSEosRequestServicesClient();
                sdgenreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdgenreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdgenreq.getAllEosReqeust(callcont, username, compid).parmEosRequestList
                          select new DTOEOS
                          {
                              EOSID = a.EosRequestId,
                              Date = a.RequestDate.ToShortDateString(),
                              EOSType = a.EOSRequestType.ToString(),
                              Worker = a.NameEn.Split('-')[1],
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Status = a.Workflowstatus.ToString(),
                              Reason = a.ReasonCode,
                              URL = "/EOS/ViewEOSRequestbyId?EOSid=" + a.EosRequestId,
                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public ActionResult CreateNewEOSRequest()
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
            ESSWebService.SDSEosRequestServicesClient sdseosreq = new ESSWebService.SDSEosRequestServicesClient();
            sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdseosreq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult geteostypes()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSEosRequestServicesClient sdseosreq = new ESSWebService.SDSEosRequestServicesClient();
            sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdseosreq.getEosTypes(callcont, compid).parmRequestIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }

        public string CreateEosRequest(string reason, string eostype, string comment,string lastworkingday,string noticestartdate)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSEosRequestServicesClient sdseosreq = new ESSWebService.SDSEosRequestServicesClient();
                sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                ESSWebService.EosRequest _creeosreq = new ESSWebService.EosRequest();
                sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                _creeosreq.Comments = comment;
                _creeosreq.RequestDate = DateTime.Now;
                _creeosreq.WorkerRecid = username;
                _creeosreq.EOSRequestType = eostype;
                _creeosreq.ReasonCode = reason;
                _creeosreq.LastWorkingDate = new DateTime(Convert.ToInt32(lastworkingday.Split('/')[2]), Convert.ToInt32(lastworkingday.Split('/')[1]), Convert.ToInt32(lastworkingday.Split('/')[0]));
                _creeosreq.NoticeStartDate = new DateTime(Convert.ToInt32(noticestartdate.Split('/')[2]), Convert.ToInt32(noticestartdate.Split('/')[1]), Convert.ToInt32(noticestartdate.Split('/')[0]));
                string g = sdseosreq.createEosRequest(callcont, _creeosreq, compid);
                return g;
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";

            }
        }

        public ActionResult SubmittedEOSRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSEosRequestServicesClient sd = new ESSWebService.SDSEosRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sd.getPendingWorkflowList(callcont, username).parmEosRequestList
                          select new DTOEOS
                          {
                              EOSID = a.EosRequestId,
                              Date = a.RequestDate.ToShortDateString(),
                              EOSType = a.EOSRequestType.ToString(),
                              Worker = a.NameEn.Split('-')[1],
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Status = a.Workflowstatus.ToString(),
                              Reason = a.ReasonCode,
                              URL = "/EOS/ViewEOSRequestbyId?EOSid=" + a.EosRequestId,
                          }).ToList();

                return View(ss.OrderByDescending(x=>x.EOSID).ToList());


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }

        public string ApproveEOSRequestbyRequestNumber(string eosid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSEosRequestServicesClient sdleavereq = new ESSWebService.SDSEosRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.EosRequest _crleavereq = new ESSWebService.EosRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmEosRequestList.Where(x => x.EosRequestId == eosid).FirstOrDefault();
                return sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, compid);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string RejecteEOSRequestbyRequestNumber(string eosid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSEosRequestServicesClient sdleavereq = new ESSWebService.SDSEosRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.EosRequest _crleavereq = new ESSWebService.EosRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmEosRequestList.Where(x => x.EosRequestId == eosid).FirstOrDefault();

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
            ESSWebService.SDSEosRequestServicesClient sdsworkcom = new ESSWebService.SDSEosRequestServicesClient();
            sdsworkcom.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdsworkcom.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            ESSWebService.SDSWorkflowApprovalWorkItemHistory workflowHistory = sdsworkcom.getWorkflowHistory(callcont, requesid, compid);
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