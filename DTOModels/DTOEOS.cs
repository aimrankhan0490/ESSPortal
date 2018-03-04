using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOEOS
    {
        public string EOSID { get; set; }
        public string Date { get; set; }
        public string EOSType { get; set; }
        public string Worker { get; set; }
        public string PersonalNumber { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string URL { get; set; }
    }
}