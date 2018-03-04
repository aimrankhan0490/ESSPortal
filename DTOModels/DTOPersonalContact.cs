using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOPersonalContact
    {
        public string Name { get; set; }
        public string RelationShip { get; set; }
        public string EmergencyContact { get; set; }
        public string Dependent { get; set; }
        public string ExtensionData { get; set; }
    }
}