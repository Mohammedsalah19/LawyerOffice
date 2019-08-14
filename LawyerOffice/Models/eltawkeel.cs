using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class eltawkeel
    {

        [Key]
        public int id { get; set; }
        [DisplayName("رقم التوكيل ")]

        public int number { get; set; }
        [DisplayName("جهه التوثيق ")]

        public string place { get; set; }
        [DisplayName("تاريخ التوثيق")]

        public DateTime date { get; set; }
        [DisplayName("ملاحظات")]

        public string notes { get; set; }
    }
}