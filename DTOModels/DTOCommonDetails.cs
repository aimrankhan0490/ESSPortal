using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOCommonDetails
    {
        public string PropertyName { get; set; }
        public string PropertyDetails { get; set; }
        public string DetailsKey { get; set; }
        public List<string> Headers { get; set; }
        public string URL { get; set; }
        public DataTable TableData { get; set; }
    }
}