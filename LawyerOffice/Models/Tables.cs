using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Tables
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string values { get; set; }
    }
}