using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOGeneralRequests
    {
        public string TransactionId { get; set; }
        public string RequestId { get; set; }
        public string RequestgroupId { get; set; }
        public string Transactiondate { get; set; }
        public string PersonalNumber { get; set; }
        public string ReasonCode { get; set; }
        public string State { get; set; }
        public string ReqNature { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}