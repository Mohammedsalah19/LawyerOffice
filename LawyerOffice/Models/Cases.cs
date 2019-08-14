using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Cases
    {
        [Key]
        public int id { get; set; }
        [DisplayName("رقم القضيه")]

        [Required(ErrorMessage = "ادخل رقم القضيه")]

        public int numberOfCase { get; set; }
        [DisplayName("رقم الحفظ ")]
        [Required(ErrorMessage = "ادخل رقم الحفظ")]

        public int numberOfFile { get; set; }
        [DisplayName("عنوان الموكل ")]
        [Required(ErrorMessage = "ادخل عنوان الموكل")]


        public string AddressofClient { get; set; }
        [DisplayName("نوع الموكل ")] //مدعى ام مدعى عليه

        public string TypeofClient { get; set; }
        [DisplayName(" رقم التوكيل لسنه")]
 

        public int NumberoOfTawkeel { get; set; }
        [DisplayName("نوع التوكيل")]

        public string TypeoOfTawkeel { get; set; }
        [DisplayName("درجه التقاضى")]

        public string degreeOfCase { get; set; }
        [DisplayName("تاريخ التوكيل")]
        [Required(ErrorMessage = "ادخل تاريخ التوكيل او القضيه")]


        public DateTime dateofElkaeed { get; set; }
        [DisplayName("اسم الخصم")]
        [Required(ErrorMessage = "ادخل رقم القضيه")]


        public string nameofGuillty { get; set; }
        [DisplayName("عنوان الخصم")]
        [Required(ErrorMessage = "ادخل رقم القضيه")]

        public string AddressoFGuility { get; set; }
        [DisplayName("نوع الخصم")] // مدعى ام مدعى عليه

        public string TypeofGuility { get; set; }
        [DisplayName("اسم المحكمه")]
        [Required(ErrorMessage = "ادخل اسم المحكمه")]

        public string CourtName { get; set; }
        [DisplayName(" الدائره")]
        public string Eldarea { get; set; }
         [DisplayName("موضوع القضيه")]
        [Required(ErrorMessage = "موضوع القضيه")]

        public string subject { get; set; }
        [DisplayName("اسم الموكل ")]

        public int clientID { get; set; }

        [DisplayName("اسم المحامى")]
        [Required(ErrorMessage = "ادخل اسم المحامى")]

        public string employeeName { get; set; }
        [Display(Name = "حاله القضيه")]
        public string IsEnabled { get; set; }
        [Display(Name = "قيمه الاتعاب")]
        [Required(ErrorMessage = "ادخل اتعاب القضيه")]

        public int money { get; set; }
        [Display(Name = "نوع المحكمه")]

        public int CourtType { get; set; }
        

    }
}