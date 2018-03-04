using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class EmployeeLoansController : Controller
    {
        // GET: EmployeeLoans
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllLoansbyEmployee()
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();

                ESSWebService.SDSLoanRequestServicesClient sdloan = new ESSWebService.SDSLoanRequestServicesClient();
                sdloan.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdloan.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                // sdloan.getAllLoanReqeust(callcont, username, compid).parmLoanRequestList;
                return RedirectToAction("Home", "Account");
            }

            else
            {
                return RedirectToAction("Home", "Account");
            }
        }
        // return _perinfo;
    }
}