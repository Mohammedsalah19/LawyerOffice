using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class hcaAndEmp
    {
        [Key]
        public int id { get; set; }
        public Employees EmpX { get; set; }
        public HowCanAcess hcaX { get; set; }
    }
}