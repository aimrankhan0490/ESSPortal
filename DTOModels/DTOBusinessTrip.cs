using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOBusinessTrip
    {
        public string BusinessTripId { get; set; }
        public string RequestDate { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Destination { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string State { get; set; }
        public string URL { get; set; }
    }
}