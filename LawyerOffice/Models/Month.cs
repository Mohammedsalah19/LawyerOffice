using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Month
    {
        [Key]
        public int id { get; set; }

        public string months { get; set; }
    }
}