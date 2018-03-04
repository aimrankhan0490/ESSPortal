using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOLoanedItems
    {
        public string LoanItem { get; set; }
        public string Description { get; set; }
        public string LoanDate { get; set; }
        public string ReturnDate { get; set; }
    }
}