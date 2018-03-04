using ESSPortal.DTOModels;
using System.Web.Http;
using System.Web.Http.Description;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class LeaveRequestController : Controller
    {
        // GET: LeaveRequest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LeaveRequestgrd()
        {
            return View();
        }
        [System.Web.Http.HttpGet, System.Web.Http.Description.ResponseType(typeof(DTOLeaveRequest)), System.Web.Http.Route("LeaveRequestAng/{pageNumber:int}/{pageSize:int}")]
        public ActionResult LeaveRequestAng(int pageNumber, int pageSize)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                List<DTOLeaveRequest> leavereqList = null; int recordsTotal = 0;
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.LeaveRequest).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();


                recordsTotal = ss.Count();
                //leavereqList = ss
                //                     .Skip(pageNumber)
                //                     .Take(pageSize)
                //                     .ToList();

                leavereqList = ss
                                   .ToList();
                return Json(new
                {
                    recordsTotal,
                    leavereqList
                }, JsonRequestBehavior.AllowGet);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }
        public ActionResult LeaveRequest(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.LeaveRequest).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();


                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public ActionResult LeavePlanning(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.LeavePlanning).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public ActionResult LeaveEnchasment(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.LeaveEncashment).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public ActionResult TicketRequest(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.TicketRequest).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public ActionResult TicketEnchasment(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList.Where(x => x.Leaverequestnature == ESSWebService.SDSLeaveReqNature.TicketEncashment).ToList()
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public ActionResult LeaveReturn(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).
                var ss = (from a in sd.getAllleaveReturnRequestList(callcont, username, compid).parmGeneralRequestList
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }

        public JsonResult GetLeaveRequestsByEmployeeLogIn()
        {

            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sd.getAllleaveRequestList(callcont, username, compid).parmGeneralRequestList
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.WorkerRecid.ToString(),
                              Worker = a.NameEn,
                              State = a.Workflowstatus.ToString(),
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                return Json(ss, JsonRequestBehavior.AllowGet);


            }
            else
            {
                RedirectToAction("Login", "Account");
                // return _perinfo;
                return Json("");
            }

        }

        public string SubmitLeaveReturn(string leaverequestid, string date, string comments)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                return sd.ReturnLeaveRequest(callcont, leaverequestid, new DateTime(Convert.ToInt32(date.Split('/')[2]), Convert.ToInt32(date.Split('/')[1]), Convert.ToInt32(date.Split('/')[0])), compid);

            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
            }
        }

        public ActionResult CreateLeaveRequest()
        {
            return View();
        }
        public ActionResult CreateLeavePlanning()
        {
            return View();
        }
        public ActionResult CreateLeaveEnchasment()
        {
            return View();
        }
        public ActionResult CreateTicketRequest()
        {
            return View();
        }
        public ActionResult CreateTicketEnchasment()
        {
            return View();
        }

        public ActionResult ViewRequestbyRequestId(string LeaveRequestNumber)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.LeaveRequest _leavereq = new ESSWebService.LeaveRequest();
                _leavereq = sdleavereq.getleaveRequest(callcont, LeaveRequestNumber, compid);
                _leavereq.NameAr = Workflowforrequest(LeaveRequestNumber);
                return View(_leavereq);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ViewallLeaveTravelled(string LeaveRequestNumber)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.LeaveRequest _leavereq = new ESSWebService.LeaveRequest();
                _leavereq = sdleavereq.getleaveRequest(callcont, LeaveRequestNumber, compid);
                _leavereq.NameAr = Workflowforrequest(LeaveRequestNumber);
                return View(_leavereq);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public string ApproveLeaveLequestbyRequestNumber(string leaverequest, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.LeaveRequest _crleavereq = new ESSWebService.LeaveRequest();
                // sdleavereq.getleaveRequest(callcont, leaverequest, compid);
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmGeneralRequestList.Where(x => x.Leaverequestid == leaverequest).FirstOrDefault();
                _crleavereq.Approved = ESSWebService.NoYes.Yes;
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

        public string RejectLeaveLequestbyRequestNumber(string leaverequest, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.LeaveRequest _crleavereq = new ESSWebService.LeaveRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmGeneralRequestList.Where(x => x.Leaverequestid == leaverequest).FirstOrDefault();
                _crleavereq.Approved = ESSWebService.NoYes.No;
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
        public ActionResult SubmittedRequestsbyType(string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSLeaveRequestServicesClient sd = new ESSWebService.SDSLeaveRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sd.getPendingWorkflowList(callcont, username).parmGeneralRequestList
                          select new DTOLeaveRequest
                          {
                              LeaveRequest = a.Leaverequestid,
                              LeaveType = a.Leavetypeid,
                              RequestDate = a.Transactiondate.ToShortDateString(),
                              FromDate = a.Fromdate.ToShortDateString(),
                              ToDate = a.ToDate.ToShortDateString(),
                              PersonalNumber = a.NameEn.Split('-')[0],
                              Worker = a.NameEn.Split('-')[1],
                              State = a.Workflowstatus.ToString(),
                              LeaveReqNature = ((int)a.Leaverequestnature).ToString(),
                              WorkflowRecId = a.WorkflowItem,
                              URL = "/LeaveRequest/ViewRequestbyRequestId?LeaveRequestNumber=" + a.Leaverequestid,
                              //Attachment = a.
                          }).ToList();
                if (reqnature != null && reqnature != "")
                    ss = ss.Where(x => x.LeaveReqNature == reqnature).OrderByDescending(x => x.LeaveRequest).ToList();

                return View(ss);


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }
        //public JsonResult 
        public JsonResult getrequestreasons()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
            sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdleavereq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getleavetypes()
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
            sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var leavetypes = sdleavereq.getLeaveTypeId_2(callcont, username, compid).parmleaveTypeIdList;
            return Json(leavetypes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getleavebalance(string leavetype, string fromdate, string todate)
        {
            try
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                DateTime frmdatedt = fromdate != "" ? Convert.ToDateTime(fromdate) : DateTime.Now;
                DateTime todatedt = todate != "" ? Convert.ToDateTime(todate) : DateTime.Now;
                var balance = sdleavereq.getLeaveBalance(callcont, username, leavetype, frmdatedt, todatedt, compid);
                return Json(balance, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw;
            }

        }
        public JsonResult getticketbalance(string leavetype, string nooftickets)
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
            sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var intnumtckt = nooftickets != "" ? Convert.ToInt32(nooftickets) : 0;
            var tckbalance = sdleavereq.getTicketBalance(callcont, username, intnumtckt, DateTime.Now, leavetype, compid);
            return Json(tckbalance, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAlternativeWorkers()
        {
            //sdleavereq.de
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);

            ESSWebService.SDSBusinessTripRequestServicesClient sdbusnesreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
            sdbusnesreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdbusnesreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var alternateworkers = sdbusnesreq.getAlternateWorker_Lookup(callcont, username, compid).parmList;
            return Json(alternateworkers, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public string CreateNewRequestbyNature(string fromdate, string todate, string leavetype, string contactinfo, string reason, string altwork
            , string destnat, string nooftickets, string reqvisa, string reqexitentry, string salinadvance, string comments, string reqnature)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                try
                {
                    string lreqid = "";
                    ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                    var compid = Request.Cookies["compid"].Value;
                    var username = Convert.ToInt64(Request.Cookies["user"].Value);
                    ESSWebService.SDSLeaveRequestServicesClient sdleavereq = new ESSWebService.SDSLeaveRequestServicesClient();
                    sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                    sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                    ESSWebService.LeaveRequest _crleavereq = new ESSWebService.LeaveRequest();
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);


                        byte[] thePictureAsBytes = new byte[file.ContentLength];
                        using (BinaryReader theReader = new BinaryReader(file.InputStream))

                        {
                            thePictureAsBytes = theReader.ReadBytes(file.ContentLength);
                            //_crleavereq.AttachmentImage = System.Text.Encoding.Default.GetString(thePictureAsBytes);
                        }

                    }
                    _crleavereq.Comments = comments == "" ? null : comments;
                    _crleavereq.ContactInfo = contactinfo == "" ? null : contactinfo;

                    _crleavereq.Destination = destnat == "" ? null : destnat;
                    var ss = fromdate.Split('/');
                    _crleavereq.Fromdate = new DateTime(Convert.ToInt32(fromdate.Split('/')[2]), Convert.ToInt32(fromdate.Split('/')[1]), Convert.ToInt32(fromdate.Split('/')[0]));
                    _crleavereq.ToDate = new DateTime(Convert.ToInt32(todate.Split('/')[2]), Convert.ToInt32(todate.Split('/')[1]), Convert.ToInt32(todate.Split('/')[0]));
                    if (_crleavereq.ToDate >= _crleavereq.Fromdate)
                    {
                        _crleavereq.Transactiondate = DateTime.Now;
                        _crleavereq.Leaverequestnature = ESSPortal.ESSWebService.SDSLeaveReqNature.LeaveRequest;
                        _crleavereq.Leavetypeid = leavetype == "-1" ? null : leavetype;
                        _crleavereq.WorkerRecid = username;
                        _crleavereq.Reasoncode = reason == "-1" ? null : reason;
                        //_crleavereq.

                        _crleavereq.Alternativeworker = altwork == "-1" ? 0 : Convert.ToInt64(altwork);
                        _crleavereq.Nooftickets = nooftickets == "" ? 0 : Convert.ToInt32(nooftickets);
                        _crleavereq.Requestexitentry = reqexitentry == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crleavereq.Requestvisa = reqvisa == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crleavereq.Salaryinadvance = salinadvance == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        lreqid = sdleavereq.createLeaveRequestByNature(callcont, _crleavereq, Convert.ToInt32(reqnature), compid);
                    }
                    else
                    {
                        throw new Exception("Fromdate should greated than Todate");
                    }

                    //string lreqid = "";

                    return lreqid;
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
            }
        }

        public string Workflowforrequest(string requesid)
        {
            string workcom = "";
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSLeaveRequestServicesClient sdsworkcom = new ESSWebService.SDSLeaveRequestServicesClient();
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