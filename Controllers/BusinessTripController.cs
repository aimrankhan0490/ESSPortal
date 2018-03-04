using ESSPortal.DTOModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class BusinessTripController : Controller
    {
        // GET: BusinessTrip
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusinessTripRequests()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = (from a in sdbustripreq.getAllBusinessTripReqeust(callcont, username, compid).parmBusinessTripRequestList
                          select new DTOBusinessTrip
                          {
                              BusinessTripId = a.RequestId,
                              ToDate = a.ToDaTE.ToShortDateString(),
                              FromDate = a.FromDate.ToShortDateString(),
                              Destination = a.Destination,
                              PersonalNumber = a.NameEn.Split('-')[0],
                              RequestDate = a.RequestDate.ToShortDateString(),
                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/BusinessTrip/ViewBusinessTripbyRequestbyId?requestid=" + a.RequestId,

                          }).ToList();
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public ActionResult ViewBusinessTripbyRequestbyId(string requestid)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var ss = sdbustripreq.getBusinessTripReqeust(callcont, requestid, compid);
                ss.NameAr = Workflowforrequest(requestid);
                return View(ss);
            }
            else
            {
                return RedirectToAction("Login", "Account");

                // return _perinfo;
            }
        }

        public ActionResult CreateNewBusinessTripRequest()
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
            ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
            sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var retreasons = sdbustripreq.getReasonCodes(callcont, compid).parmReasonCodeList;
            return Json(retreasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAlternativeWorkers()
        {
            //sdleavereq.de
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
            sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var alternateworkers = sdbustripreq.getAlternateWorker_Lookup(callcont, username, compid).parmList;
            return Json(alternateworkers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDestinations()
        {
            //sdleavereq.de
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
            sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            var alternateworkers = sdbustripreq.getDestination_Lookup(callcont, username,compid).parmList;
            return Json(alternateworkers, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string CreateNewBusinessTrip(string fromdate, string todate, string reason, string altwork
           , string destnat, string reqvisa, string reqexitentry, string accomdation, string meals, string transportation, string perdeemamount, string comments)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                try
                {
                    ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                    var compid = Request.Cookies["compid"].Value;
                    var username = Convert.ToInt64(Request.Cookies["user"].Value);
                    ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                    sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                    sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                    ESSWebService.BusinessTripRequest _crbustripreq = new ESSWebService.BusinessTripRequest();
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
                    }
                    var ss = fromdate.Split('/');
                    _crbustripreq.Comments = comments;
                    _crbustripreq.FromDate = new DateTime(Convert.ToInt32(fromdate.Split('/')[2]), Convert.ToInt32(fromdate.Split('/')[1]), Convert.ToInt32(fromdate.Split('/')[0]));
                    _crbustripreq.ToDaTE = new DateTime(Convert.ToInt32(todate.Split('/')[2]), Convert.ToInt32(todate.Split('/')[1]), Convert.ToInt32(todate.Split('/')[0]));

                    string lreqid = "";
                    // _crleavereq.Destination = destnat;
                    if (_crbustripreq.ToDaTE >= _crbustripreq.FromDate)
                    {
                        _crbustripreq.RequestDate = DateTime.Now;
                        _crbustripreq.AlternativeWorker = altwork == "-1" ? 0 : Convert.ToInt64(altwork);
                        _crbustripreq.WorkerRecid = username;
                        _crbustripreq.ReasonCode = reason == "-1" ? null : reason;
                        _crbustripreq.NoOfDays = (_crbustripreq.ToDaTE - _crbustripreq.FromDate).Days + 1;
                        //_crleavereq.
                        //_crleavereq.Leavetypeid = leavetype;
                        //_crleavereq.Alternativeworker = altwork;
                        _crbustripreq.Destination = destnat;
                        _crbustripreq.RequestExistEntry = reqexitentry == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crbustripreq.RequestVisa = reqvisa == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crbustripreq.AccomodationProvided = accomdation == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crbustripreq.MealProvided = meals == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        _crbustripreq.TransportationProvided = transportation == "1" ? ESSPortal.ESSWebService.NoYes.Yes : ESSPortal.ESSWebService.NoYes.No;
                        //_crbustripreq.PerdeemAmount = Convert.ToInt64(perdeemamount);
                        lreqid = sdbustripreq.createBusinessTripRequest(callcont, _crbustripreq, compid);
                    }
                    else
                        throw new Exception("Todate should be greater than fromdate ");

                    // string lreqid = "";

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


        public string ApproveBusinessTripbyId(string id, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                try
                {
                    ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                    var compid = Request.Cookies["compid"].Value;
                    var username = Convert.ToInt64(Request.Cookies["user"].Value);
                    ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                    sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                    sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                    ESSWebService.BusinessTripRequest _crbustripreq = new ESSWebService.BusinessTripRequest();
                    _crbustripreq = sdbustripreq.getPendingWorkflowList(callcont, username).parmBusinessTripRequestList.Where(x => x.RequestId == id).FirstOrDefault();

                    return sdbustripreq.Approve(callcont, _crbustripreq.WorkflowItem, username, compid);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public string RejectBusinessTripbyId(string id, string Comment)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                ESSWebService.BusinessTripRequest _crbustripreq = new ESSWebService.BusinessTripRequest();
                _crbustripreq = sdbustripreq.getPendingWorkflowList(callcont, username).parmBusinessTripRequestList.Where(x => x.RequestId == id).FirstOrDefault();

                //sdleavereq.Approve(callcont, _crleavereq.WorkflowItem, username, Comment);
                return sdbustripreq.Reject(callcont, _crbustripreq.WorkflowItem, username, Comment);
            }
            else
            {
                RedirectToAction("Login", "Account");
                return "";
                // return _perinfo;
            }
        }

        public ActionResult SubmittedBusinessTripRequest()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                var compid = Request.Cookies["compid"].Value;
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                ESSWebService.SDSBusinessTripRequestServicesClient sdbusinesstripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
                sdbusinesstripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdbusinesstripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                //sd.getPendingWorkflowList()
                // sd.Approve()
                //  sd.getAllleaveRequestList(callcont, username, compid).

                var ss = (from a in sdbusinesstripreq.getPendingWorkflowList(callcont, username).parmBusinessTripRequestList
                          select new DTOBusinessTrip
                          {
                              BusinessTripId = a.RequestId,
                              ToDate = a.ToDaTE.ToShortDateString(),
                              FromDate = a.FromDate.ToShortDateString(),
                              Destination = a.Destination,
                              PersonalNumber = a.NameEn.Split('-')[0],
                              RequestDate = a.RequestDate.ToShortDateString(),
                              Name = a.NameEn.Split('-')[1],
                              State = a.WorkflowStatus.ToString(),
                              URL = "/BusinessTrip/ViewBusinessTripbyRequestbyId?requestid=" + a.RequestId,

                          }).ToList();

                return View(ss.OrderByDescending(x => x.BusinessTripId).ToList());


            }
            else
            {
                return RedirectToAction("Login", "Account");
                // return _perinfo;
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

        public decimal GetperdiemAmount(string destinatioin, string Transportation, string housing, string food, string fromdate, string todate)
        {
            ESSWebService.CallContext callcont = new ESSWebService.CallContext();
            var compid = Request.Cookies["compid"].Value;
            var username = Convert.ToInt64(Request.Cookies["user"].Value);
            var FromDate = new DateTime();
            var ToDaTE = new DateTime();

            ESSWebService.SDSBusinessTripRequestServicesClient sdbustripreq = new ESSWebService.SDSBusinessTripRequestServicesClient();
            sdbustripreq.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
            sdbustripreq.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
            if (fromdate.Length == 10 && todate.Length == 10)
            {
                FromDate = new DateTime(Convert.ToInt32(fromdate.Split('/')[2]), Convert.ToInt32(fromdate.Split('/')[1]), Convert.ToInt32(fromdate.Split('/')[0]));
                ToDaTE = new DateTime(Convert.ToInt32(todate.Split('/')[2]), Convert.ToInt32(todate.Split('/')[1]), Convert.ToInt32(todate.Split('/')[0]));
            }



            var retdec = sdbustripreq.getPerdiemAmount(callcont, username, destinatioin, (fromdate.Length == 10 && todate.Length == 10) ? ((ToDaTE - FromDate).Days + 1) : 1, housing == "1" ? ESSWebService.NoYes.Yes : ESSWebService.NoYes.No,
                food == "1" ? ESSWebService.NoYes.Yes : ESSWebService.NoYes.No, Transportation == "1" ? ESSWebService.NoYes.Yes : ESSWebService.NoYes.No, compid);
            return retdec;
        }
    }
}