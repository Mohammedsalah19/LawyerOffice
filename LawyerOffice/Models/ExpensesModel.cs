using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class ExpensesModel
    {
        [Key]
        public int id { get; set; }

        [DisplayName("الصنف")]

        public string  Expensestype { get; set; }
        [DisplayName("القيمه")]

        public int values { get; set; }
        [DisplayName("ملاحظات")]

        public string Comment { get; set; }
        [DisplayName("المكتب")]

        public string office { get; set; }
        [DisplayName("التاريخ")]

        public DateTime date { get; set; }
    }

}