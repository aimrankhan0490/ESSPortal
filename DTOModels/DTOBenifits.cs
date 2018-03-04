using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOBenifits
    {
        public string BenifitID { get; set; }
        public string BenifitType { get; set; }
        public string xAmount { get; set; }
        public string Percent { get; set; }
        public string ExtensionData { get; set; }
    }
}