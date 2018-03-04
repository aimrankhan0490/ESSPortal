using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOMyAttendance
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Event { get; set; }
        public string Excused { get; set; }
    }
}