using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class elmohdar
    {
        [Key]
        public int id { get; set; }
        [DisplayName("اسم المحضر")]

        public string Name { get; set; }
        [DisplayName("قلم المحضر")]

        public string MohdarPen { get; set; }
        [DisplayName("تاريخ الجلسه")]

        public DateTime GalsaDate { get; set; }
        [DisplayName("المحامى الحاضر")]

        public string lawyer { get; set; }
        [DisplayName("ملاحظات")]
        public string note { get; set; }
    }
}