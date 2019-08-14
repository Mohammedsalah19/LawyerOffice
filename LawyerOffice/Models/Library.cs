using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Library
    {
        [Key]
        public int id { get; set; }
        [DisplayName("اسم الكتاب")]
        public string BookName { get; set; }
        [DisplayName("اختار الكتاب")]

        public string BookPath { get; set; }
        [DisplayName("ناشر الكتاب")]

        public string Publisher { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public HttpPostedFileBase BookFile { get; set; }
    }
}