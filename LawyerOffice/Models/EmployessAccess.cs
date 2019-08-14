using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class EmployessAccess
    {

        [Key]
        public int id { get; set; }
 

        public string userName { get; set; }

        public string passsword { get; set; }

        public virtual Employees EmpID { get; set; }
    }
}