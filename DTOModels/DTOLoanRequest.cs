using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.ESSWebService
{
    public partial class LeaveRequest
    {
        public int noofdays
        {
            get
            {
                int ss = ((ToDate - Fromdate).Days + 1);
                return ss;
            }
        }
    }
}