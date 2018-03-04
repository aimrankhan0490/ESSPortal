using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.DTOModels
{
    public class DTOContacts
    {
        public string ContactNumber { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public string IsPrimary { get; set; }
        public string IsPrvate { get; set; }
        public string Type { get; set; }
    }
}