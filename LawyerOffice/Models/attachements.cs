using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class attachements
    {

        [Key]
        public int Atachment_id { get; set; }
        [DisplayName("اسم الملف")]

        public string titlte { get; set; }
        [DisplayName("اختار الملف")]

        public string ImagePath { get; set; }
        [DisplayName("نوع الملف")]

        public int AttType { get; set; }
        public int CaseID { get; set; }

        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }

        
    }
}