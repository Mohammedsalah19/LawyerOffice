using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "ادخل اسم الموكل")]
        [DisplayName("أسم الموكل")]
        public string Name { get; set; }
        [DisplayName(" الجنسيه")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "ادخل عنوان الموكل")]

        [DisplayName(" العنوان")]

        public string Address { get; set; }
        [DisplayName(" الديانه")]
        [Required(ErrorMessage = "ادخل ديانه الموكل")]


        public string eldiana { get; set; }
        [Required(ErrorMessage = "ادخل رقم تليفون الموكل")]
        [DisplayName(" رقم التليفون")]

 
        public string Mobile { get; set; }
        [DisplayName(" البريد الاكترونى")]
        public string Email { get; set; }

        [DisplayName("ملاحظات ")]
        public string Notes { get; set; }

        [DisplayName("المحامى")]

        [Required(ErrorMessage = "ادخل اسم المحامى المسؤول")]

        public int employeeName { get; set; }
        public IEnumerable<Cases> caseID { get; set; }


    }
}