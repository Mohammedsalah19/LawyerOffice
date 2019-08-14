using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class SalaryWithEmpAndMonth
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Month> MonthsX { get; set; }

        public IEnumerable<Employees>EmpX { get; set; }

        public IEnumerable<Salaries> salaryX { get; set; }
        public IEnumerable<Offices> officeX { get; set; }
        public IEnumerable<Jobs> jobx { get; set; }

    }
}