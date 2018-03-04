using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.Controllers
{
    public class DTOMedicalInsurance
    {
        public string DentalLimit { get; set; }
        public string DependentNameArabic { get; set; }
        public string DependentNameEnglish { get; set; }
        public string Endurance { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MembershipId { get; set; }
        public string OccuralLimit { get; set; }
        public string PolicyLevel { get; set; }
        public string PolicyFor { get; set; }
        public string PolicyNumber { get; set; }
        public string PregnencyCover { get; set; }
        public string Room { get; set; }
        public string TotalLimit { get; set; }
        public string WorkerName { get; set; }
        public string WorkerRecId { get; set; }
    }
    public class DTOMedicalInsurancelst
    {
        public string DependentName { get; set; }
        public string DependentNameEnglish { get; set; }
        //  public string Endurance { get; set; }
        public string MembershipId { get; set; }
        public string PolicyLevel { get; set; }
       // public string PolicyFor { get; set; }
        public string PolicyNumber { get; set; }
        public string PregnencyCover { get; set; }
        public string Room { get; set; }
        public string TotalLimit { get; set; }
    }
}