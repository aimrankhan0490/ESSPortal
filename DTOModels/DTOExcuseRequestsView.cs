using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESSPortal.ESSWebService
{
    public partial class ExcuseRequest
    {
        public string FromTimeReadOnly
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds(FromTime);

                return string.Format("{0:D2}h:{1:D2}m",
                                t.Hours,
                                t.Minutes

                               );

            }
        }

        public string ToTimeReadOnly
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds(ToTime);

                return string.Format("{0:D2}h:{1:D2}m",
                                t.Hours,
                                t.Minutes

                               );
            }
        }
    }
}