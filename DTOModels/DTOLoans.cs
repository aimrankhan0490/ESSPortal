using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOLoans
    {
        public string LoanRequestId { get; set; }
        public string LoanType { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string PersonalNumber { get; set; }
        public string LoanRequested { get; set; }
        public string MonthlyInstallment { get; set; }
        public string NoofInstallment { get; set; }
        public string ReasonCode { get; set; }
        public string State { get; set; }
        public string URL { get; set; }
    }
}