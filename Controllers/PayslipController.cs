using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESSPortal.Controllers
{
    public class PayslipController : Controller
    {
        // GET: Payslip
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PayslipbyEmployee(string month, string year)
        {
            if (Request.Cookies["user"] != null && Request.Cookies["compid"] != null)
            {
                int monthval;
                int yearval;
                monthval = month != null && month != "" ? Convert.ToInt32(month) : DateTime.Now.AddMonths(-1).Month;
                yearval = year != null && year != "" ? Convert.ToInt32(year) : DateTime.Now.AddMonths(-1).Year;

                var username = Convert.ToInt64(Request.Cookies["user"].Value);
                var compid = Request.Cookies["compid"].Value;
                ESSWebService.SDSPaySlipInfoServiceClient sdspayslip = new ESSWebService.SDSPaySlipInfoServiceClient();
                ESSWebService.CallContext callcont = new ESSWebService.CallContext();
                sdspayslip.ClientCredentials.Windows.ClientCredential.Domain = "Soletechs";
                sdspayslip.ClientCredentials.Windows.ClientCredential = new NetworkCredential("webapp", "pass" + '"' + "word123");
                var hdr = sdspayslip.getPaySlipInfo(callcont, username, yearval, monthval, compid);
                var lines = sdspayslip.getPaySlipLineInfoList(callcont, username, yearval, monthval, compid).parmpaySlipLineInfoList;

                DTOModels.DTOPayslip payslip = new DTOModels.DTOPayslip();
                payslip.FromDate = hdr.Fromdate.ToShortDateString();
                payslip.ToDate = hdr.Todate.ToShortDateString();
                payslip.Month = hdr.Months.ToString();
                payslip.Year = hdr.Year.ToString();
                payslip.Paycycle = hdr.PaycycleId;
                payslip.Paygroup = hdr.PayGroup;
                payslip.TotalDeductions = hdr.TotalDeductions.ToString("#,##0");
                payslip.TotalEarnings = hdr.TotalEarnings.ToString("#,##0");
                payslip.NetAmount = hdr.Netsalary.ToString("#,##0");
                payslip.Earnings = (from a in lines
                                    where (int)a.SourceType == 0
                                    select new DTOModels.DTOEarnings
                                    {


                                        Amount = Convert.ToDecimal(a.Amount).ToString("#,##0"),
                                        EarningsType = a.Paymenttype,
                                        EarningsTypeID = a.PayCode,
                                    }).ToList();
                payslip.Deductions = (from a in lines
                                      where (int)a.SourceType == 1
                                      select new DTOModels.DTODeductions
                                      {
                                          Amount = Convert.ToDecimal(a.Amount).ToString("#,##0"),
                                          DeductionType = a.Paymenttype,
                                          DeductionTypeID = a.PayCode,
                                      }).ToList();
                return View("Payslip", payslip);
            }
            else
            {
                return RedirectToAction("Login");
                // return _perinfo;
            }
        }
    }
}