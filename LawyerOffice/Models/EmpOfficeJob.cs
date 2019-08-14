using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class EmpOfficeJob
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Employees> Empx { get; set; }
        public Employees EmpY { get; set; }

        public IEnumerable<Offices> OfficeX { get; set; }

        public IEnumerable<Jobs> JobX { get; set; }

        public IEnumerable<HowCanAcess> hcaX { get; set; }
    }
}