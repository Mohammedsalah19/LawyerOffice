using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class CourtType
    {
        [Key]
        public int id { get; set; }
        [DisplayName("اسم المحكمه")]

        public string TypeName { get; set; }

    }
}