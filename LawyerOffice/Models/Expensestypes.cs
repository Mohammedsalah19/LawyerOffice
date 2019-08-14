using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Expensestypes
    {
        [Key]
        public int id { get; set; }
        [DisplayName("الصنف")]

        public string catogery { get; set; }


    }
}