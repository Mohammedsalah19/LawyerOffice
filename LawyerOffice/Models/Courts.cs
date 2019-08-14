using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Courts
    {

        // لينك المحاكم
        // https://qadaya.net/?p=5505

        public int id { get; set; }
        [DisplayName("اسم المحكمه")]

        public string CourtName { get; set; }
        [DisplayName("نوع المحكمه")]

        public string CourtType { get; set; }
        public int cityid { get; set; }

    }
}