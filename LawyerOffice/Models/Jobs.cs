using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Jobs
    {
        [Key]
        public  int id { get; set; }
        [DisplayName("الوظيفه")]

        public string jobs { get; set; }

 
    }
}