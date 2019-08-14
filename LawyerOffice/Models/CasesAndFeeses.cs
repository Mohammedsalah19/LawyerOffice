using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class CasesAndFeeses
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Cases> CasesX { get; set; }
        public IEnumerable<fees> FeesesX { get; set; }

    }
}