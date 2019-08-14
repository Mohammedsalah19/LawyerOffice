using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Salaries
    {
        [Key]
        public int id { get; set; }
        [DisplayName("الموظف")]

        public string Employee { get; set; }
        [DisplayName(" المسؤول")]

        public string empSupplyer { get; set; }
        [DisplayName(" المرتب")]

        public int salary { get; set; }
 
        [DisplayName("الشهر")]

        public string month { get; set; }

        public DateTime Date { get; set; }

    }
}