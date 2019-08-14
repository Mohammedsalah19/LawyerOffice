using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class CasesCourtsEmployees
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Client>clientX { get; set; }
        public IEnumerable<Courts> courtsX{ get; set; }

        public IEnumerable<Cases>casesX { get; set; }
        public IEnumerable<Employees> EmpX { get; set; }

    }
}