using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOCommonDetailsList
    {
        public string Key { get; set; }      
        public string GroupKey { get; set; }
        public string DisplayName { get; set; }
        public string WorkerName { get; set; }
        
        public List <DataTable> tabledata { get; set; }
        public string PersonalNumber { get; set; }
        public string EnglishName { get; set; }
        public string Gender { get; set; }
        public string JoiningDate { get; set; }
        public string OldID { get; set; }
        public string ArabicName { get; set; }
        public string MaritalStatus { get; set; }
        public string ProbhotionEndDate { get; set; }
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string Garde { get; set; }
        public string SalaryScale { get; set; }
        public string Paygroup { get; set; }
        public string BasicSalary { get; set; }
        public string AnualLeave { get; set; }
        public string NoofTickets { get; set; }
      
        public string TicketRate { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public string TicketClass { get; set; }

        public string PositionId { get; set; }
        public string PositionDescription { get; set; }
        public string Department { get; set; }
        public string ReporttoPosition { get; set; }
        public string ReporttoPositionManagerId { get; set; }
        public string PositioinCompensationName { get; set; }
        public string ReportToLineManagerName { get; set; }
        public string PositionType { get; set; }
        public string PositionCompensation { get; set; }
        public string JobId { get; set; }
        public string JobType { get; set; }
        public string JobFunction { get; set; }



        public string Name { get; set; }
        public string MembershipId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Endurance { get; set; }
        public string DentalLimit { get; set; }
        public string OccuralLimit { get; set; }
        public string PolicyLevel { get; set; }
        public string PolicyNumber { get; set; }
        public string Room { get; set; }
        public string TotalLimit { get; set; }



    }
}