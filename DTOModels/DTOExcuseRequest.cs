using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOExcuseRequest
    {
        public string RequestId { get; set; }
        public string ExcuseType { get; set; }
        public string ExcuseId { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string State { get; set; }
        public string URL { get; set; }
    }
}