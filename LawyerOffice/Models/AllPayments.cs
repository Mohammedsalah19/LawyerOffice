using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class AllPayments
    {

        public int id { get; set; }
        public IEnumerable<Cases> caseX { get; set; }
        public IEnumerable<Employees> EmpX { get; set; }
        public IEnumerable<Salaries> salaryX { get; set; }
        public IEnumerable<fees> feesX { get; set; }

    }
}