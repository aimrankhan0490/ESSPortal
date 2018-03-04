using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESSPortal;
using System.Web.Mvc;

namespace ESSPortal.DTOModels
{
    public class DTOPersonalInfo
    {
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string FirstName { get; set; }
        public string Companyname { get; set; }
        public string Departmentname { get; set; }
        public string OldID { get; set; }
        public string NewID { get; set; }
        public string JobDesc { get; set; }
        public string JobType { get; set; }
        public string PositionDesc { get; set; }
        public string PositionId { get; set; }
        public string PersonalNumber { get; set; }
        public string Department { get; set; }
        public string JoiningDate { get; set; }
        public string ProbationEndDate { get; set; }
        public string Age { get { return BirthDate != "-" ? (DateTime.Now.Year - Convert.ToDateTime( BirthDate).Year).ToString() : ""; } }
        public string BirthDate { get; set; }
        public List<DTODashBoard> dashboardlist { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public string MaritalStatusDesc { get { return MaritalStatus == 1 ? "Single" : "Married"; } }
        public string GenderDesc { get { return Gender == 1 ? "Male" : "Female"; } }
        public int NoofDependencies { get; set; }
        public string ImageArray { get; set; }
        public int CountAddress { get; set; }
        public int CountBank { get; set; }
        public int CountContact { get; set; }
        public int CountDependents { get; set; }
        public int CountExcuseRequest { get; set; }
        public int CountBenifits { get; set; }
        public int CountLetter { get; set; }
        public int tcktencahscount { get; set; }
        public int absencecount { get; set; }
        
        public int CountIdentification { get; set; }
        public int CountMedicalInsuarance { get; set; }
        public int CountLeaveRequest { get; set; }
        public int CountTicketRequest { get; set; }
        public int CountSkills { get; set; }
        public string DatePayslip { get; set; }
        public string DatePayslipPrevious { get; set; }
        
        public string YearMonth { get; set; }
        public string Datepay { get; set; }
        public string DayPaid { get; set; }

        public string DatePayslipPrev { get; set; }
        public string YearMonthPrev { get; set; }
        public string DatepayPrev { get; set; }
        public string DayPaidPrev { get; set; }
        public int AttendanceCount { get; set; }
        public int LoanedItemCount { get; set; }
        public int MedicalInsuranceCount { get; set; }
        public int MyAbsenceCount { get; set; }
        public int MyExcuseCount { get; set; }
        public int MyEOSCount { get; set; }
        public int MyGeneralCount { get; set; }
        public int MyLoanCount { get; set; }
        public int MyOverTimeCount { get; set; }
        public int MyLeavePlanningCount { get; set; }
        public int MyLeaveReturnCount { get; set; }
        public int MyEnchasmentCount { get; set; }
        public int MyTicketRequestCount { get; set; }
        public int MyTicketEnchasmentCount { get; set; }
        public int MyLoanRequestCount { get; set; }
        public int MyRequestCount { get; set; }
        public int MyBusinessTripCount { get; set; }

        public string ImagePath { get { return ImageArray != null && ImageArray != "" ? string.Format("data:image/jpeg;base64,{0}", ImageArray) : "/assets/images/avatars/profile-pic.jpg"; } }
        //public FileContentResult ImagePath { get { return ImageArray.Length != 0 ? new FileContentResult(ImageArray, "image/jpeg") : null; } }
    }
}