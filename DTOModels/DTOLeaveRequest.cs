using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOLeaveRequest
    {
        public string LeaveRequest { get; set; }
        public string LeaveType { get; set; }
        public string RequestDate { get; set; }
        public string PersonalNumber { get; set; }
        public string Worker { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string State { get; set; }
        public string LeaveReqNature { get; set; }
        public System.Guid WorkflowRecId { get; set; }
        public string URL { get; set; }
        // public string Attachment { get; set; }
    }
}