using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Employees
    {
        [Key]
        public int id { get; set; }
        [DisplayName("اسم الموظف")]

        public string employeeName { get; set; }
        [DisplayName("عنوان الموظف")]


        public string Address { get; set; }

        [DisplayName("رقم التليفون")]

        public string phone { get; set; }

        [DisplayName("الوظيفه")]
        public string jobs { get; set; }

        [DisplayName("المرتب")]

        public int Salary { get; set; }

        [DisplayName("المكتب")]
        public string OfficeName { get; set; }
        [DisplayName("كلمة المرور")]

        [DataType(DataType.Password)]

        public string Paswword { get; set; }

 



    }
}