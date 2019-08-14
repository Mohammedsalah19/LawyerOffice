using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Offices
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("اسم المكتب")]

        public string OfficeName { get; set; }
        [DisplayName("تفاصيل اخرى")]

        public string Description { get; set; }

    }
}