using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class MyAttendanceController : Controller
    {
        // GET: MyAttendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAttendancebyDate(string FromDate, string ToDate, string ByTeam)
        {
            try
            {
                if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
                {
                    var username = Convert.ToInt64(Request.Cookies["user"].Value);
                    var compid = Request.Cookies["compid"].Value;
                    ESSWebService.SDSAttendanceInfoServicesClient sattendance = new ESSWebService.SDSAttendanceInfoServicesClient();
                    ESSWebService.CallContext callcont = new ESSWebService.CallContext();

                    sattendance.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                    sattendance.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                    var from = new DateTime(2017, 10, 11);
                    var to = new DateTime(2017, 11, 11);
                    var attlist = sattendance.getAttendanceList(callcont, username, from, to).parmAttendanceList;
                    var toreturn = (from a in attlist
                                    select new DTOModels.DTOMyAttendance
                                    {
                                        Date = a.Date.ToShortDateString(),
                                        Time = a.Time,
                                        Event = a.Event,
                                        Excused = a.IsExcuse == 0 ? "No" : "Yes"
                                    }).ToList();
                    return View("MyAttendance", toreturn);
                }
                else
                {
                    //return View("Account/Login");
                    return RedirectToAction("Login", "Account");
                    // return _perinfo;
                }
            }
            catch (Exception ex)
            {

                // return Redirect("Account/Home");
                //return View("Account/Login");
                return RedirectToAction("Home", "Account");

            }


        }
    }
}