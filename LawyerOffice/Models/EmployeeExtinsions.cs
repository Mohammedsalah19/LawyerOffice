using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class EmployeeExtinsions
    {

        [Key]
        public int id { get; set; }
        public IEnumerable<Jobs> jobX { get; set; }
        public IEnumerable<Offices> officeX { get; set; }
        public IEnumerable<Employees> EmployeesX { get; set; }

    }
}