using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class WorkFlowController : Controller
    {
        // GET: WorkFlow
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ViewWorkflowHistorybyType(string Type ,string Id)
        {
            return View();
        }

    }
}