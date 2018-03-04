using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class ExcuseRequestController : Controller
    {
        // GET: ExcuseRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AbsenceRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Absence, compid).parmExcuseRequestList
                          select new DTOExcuseRequest
                          {
                              RequestId = a.RequestId,
                              ExcuseType = ESSWebService.SDSExcuseNature.Absence.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              ExcuseId = a.ExcuseType,
                              PersonalNumber = a.NameEn.Split('-')[0],

                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/ExcuseRequest/ViewExcuseRequestbyId?requestid=" + a.RequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult OverTimeRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Overtime, compid).parmExcuseRequestList
                          select new DTOExcuseRequest
                          {
                              RequestId = a.RequestId,
                              ExcuseType = ESSWebService.SDSExcuseNature.Overtime.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              ExcuseId = a.ExcuseType,
                              PersonalNumber = a.NameEn.Split('-')[0],

                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/ExcuseRequest/ViewExcuseRequestbyId?requestid=" + a.RequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult ExcuseRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Excuse, compid).parmExcuseRequestList
                          select new DTOExcuseRequest
                          {
                              RequestId = a.RequestId,
                              ExcuseType = ESSWebService.SDSExcuseNature.Excuse.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              ExcuseId = a.ExcuseType,
                              PersonalNumber = a.NameEn.Split('-')[0],

                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/ExcuseRequest/ViewExcuseRequestbyId?requestid=" + a.RequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult TrainingRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdexcusereq.getAllExcuseReqeustByNature(callcont, username, ESSWebService.SDSExcuseNature.Training, compid).parmExcuseRequestList
                          select new DTOExcuseRequest
                          {
                              RequestId = a.RequestId,
                              ExcuseType = ESSWebService.SDSExcuseNature.Training.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              ExcuseId = a.ExcuseType,
                              PersonalNumber = a.NameEn.Split('-')[0],

                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/ExcuseRequest/ViewExcuseRequestbyId?requestid=" + a.RequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public ActionResult CreateAbsenceRequest()
        {
            return View();
        }
        public ActionResult CreateOverTimeRequest()
        {
            return View();
        }
        public ActionResult CreateExcuseRequest()
        {
            return View();
        }
        public ActionResult CreateTrainingRequest()
        {
            return View();
        }
        public JsonResult getreasoncodes()
        {
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var compid = Request.Cookies["compid"].Value;
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
            sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdexcusereq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getexecustypesforabsent()
        {
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var compid = Request.Cookies["compid"].Value;
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
            sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdexcusereq.getExcuseTypeIdByNature(callcont, ESSWebService.SDSExcuseNature.Absence, compid).parmExcuseTypeIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getexecustypesforExcuse()
        {
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var compid = Request.Cookies["compid"].Value;
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
            sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdexcusereq.getExcuseTypeIdByNature(callcont, ESSWebService.SDSExcuseNature.Excuse, compid).parmExcuseTypeIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getexecustypesforOvertime()
        {
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var compid = Request.Cookies["compid"].Value;
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
            sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdexcusereq.getExcuseTypeIdByNature(callcont, ESSWebService.SDSExcuseNature.Overtime, compid).parmExcuseTypeIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getexecustypesforTraining()
        {
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var compid = Request.Cookies["compid"].Value;
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            ESSWebService.SDSExcuseRequestServicesClient sdexcusereq = new ESSWebService.SDSExcuseRequestServicesClient();
            sdexcusereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdexcusereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdexcusereq.getExcuseTypeIdByNature(callcont, ESSWebService.SDSExcuseNature.Training, compid).parmExcuseTypeIdList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }

        public string CreateExcuseRequestbyNature(string reason, string execuseid, string comment,
            string excusenature, string frmtime, string totime, string requestdate)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                string g = "";
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSExcuseRequestServicesClient sdsexcreq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdsexcreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                ESSWebService.ExcuseRequest _creeosreq = new ESSWebService.ExcuseRequest();
                sdsexcreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                _creeosreq.Comments = comment;
                //_creeosreq.RequestDate = DateTime.Now;
                _creeosreq.RequestDate = new DateTime(Convert.ToInt32(requestdate.Split('/')[2]), Convert.ToInt32(requestdate.Split('/')[1]), Convert.ToInt32(requestdate.Split('/')[0]));
                _creeosreq.WorkerRecId = username;
                _creeosreq.ExcuseType = execuseid == "-1" ? null : execuseid;
                _creeosreq.ReasonCode = reason == "-1" ? null : reason;
                _creeosreq.FromTime = Helper.Helper.Timetoint(frmtime);
                _creeosreq.ToTime = Helper.Helper.Timetoint(totime);
                //_creeosreq.FromTime = 2;
                //_creeosreq.ToTime = 2;
                if (_creeosreq.ToTime >= _creeosreq.FromTime)
                {
                    g = sdsexcreq.createExcuseRequestByNature(callcont, _creeosreq, (ESSWebService.SDSExcuseNature)Convert.ToInt32(excusenature), compid);
                }
                else
                    g = "Totime should be greater than from date";
                return g;
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";

            }
        }
        public ActionResult ViewExcuseRequestbyId(string requestid)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSExcuseRequestServicesClient sdseosreq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdseosreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdseosreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = sdseosreq.getExcuseReqeust(callcont, requestid, compid);
                ss.NameAr = Workflowforrequest(requestid);
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public ActionResult SubmittedExcuseRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSExcuseRequestServicesClient sd = new ESSWebService.SDSExcuseRequestServicesClient();
                sd.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sd.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sd.getPendingWorkflowList(callcont, username).parmExcuseRequestList
                          select new DTOExcuseRequest
                          {
                              RequestId = a.RequestId,
                              ExcuseType = ESSWebService.SDSExcuseNature.Absence.ToString(),
                              Date = a.RequestDate.ToShortDateString(),
                              ExcuseId = a.ExcuseType,
                              PersonalNumber = a.NameEn.Split('-')[0],

                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/ExcuseRequest/ViewExcuseRequestbyId?requestid=" + a.RequestId,

                          }).ToList();

                return View(ss.OrderByDescending(x => x.RequestId).ToList());


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
            }
        }

        public string ApproveExcuseRequestbyRequestNumber(string requestid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSExcuseRequestServicesClient sdleavereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.ExcuseRequest _crleavereq = new ESSWebService.ExcuseRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmExcuseRequestList.Where(x => x.RequestId == requestid).FirstOrDefault();
                return sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, compid);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string RejecteExcuseRequestbyRequestNumber(string requestid, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSExcuseRequestServicesClient sdleavereq = new ESSWebService.SDSExcuseRequestServicesClient();
                sdleavereq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdleavereq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.ExcuseRequest _crleavereq = new ESSWebService.ExcuseRequest();
                _crleavereq = sdleavereq.getPendingWorkflowList(callcont, username).parmExcuseRequestList.Where(x => x.RequestId == requestid).FirstOrDefault();
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
            ESSWebService.SDSExcuseRequestServicesClient sdsworkcom = new ESSWebService.SDSExcuseRequestServicesClient();
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