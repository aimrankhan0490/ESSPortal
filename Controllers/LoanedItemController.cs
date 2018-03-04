using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class LoanedItemController : Controller
    {
        // GET: LoanedItem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLoanedItemsbyEmployee(string status)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.SDSPersonalInfoServicesClient sdperinfo = new ESSWebService.SDSPersonalInfoServicesClient();
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();

                sdperinfo.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdperinfo.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var loanequip = sdperinfo.getAllLoanedEquipList(callcont, username, true).parmAddressList;
                var toreturn = (from a in loanequip
                                select new DTOModels.DTOLoanedItems
                                {
                                    LoanItem = a.LoanItemId,
                                    LoanDate = a.LoanedDate.ToShortDateString(),
                                    Description = a.Description,
                                    ReturnDate = a.ActualReturnDate.ToShortDateString()
                                });
                return View("LoanedItem", toreturn);
            }
            else
            {
                return RedirectToAction("Home", "Account");
                // return _perinfo;
            }
        }
    }
}