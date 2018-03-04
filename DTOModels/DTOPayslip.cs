using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOPayslip
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Paygroup { get; set; }
        public string Paycycle { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NetAmount { get; set; }
        public string TotalEarnings { get; set; }
        public string TotalDeductions { get; set; }
        public List <DTOEarnings> Earnings { get; set; }
        public List <DTODeductions> Deductions { get; set; }
    }
}