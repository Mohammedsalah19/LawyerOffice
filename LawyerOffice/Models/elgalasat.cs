using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class elgalasat
    {

        [Key]
        public int id { get; set; }
        [DisplayName("رقم الجلسه")]
        [Required(ErrorMessage = "ادخل رقم الجلسه")]

        public int NumberOfGalsa { get; set; }
        [DisplayName("رقم القضيه")]

        public int NumberOfCase { get; set; }
        [DisplayName("تاريخ الجلسه")]
        [Required(ErrorMessage = "ادخل تاريخ الجلسه")]

        [DataType(DataType.DateTime)]
        public DateTime dateOfGalsa { get; set; }
 
        [DisplayName("رقم الرول")]
        [Required(ErrorMessage = "ادخل رقم الرول")]


        public string RollNumber { get; set; }
        [DisplayName("المحكمه")]
        [Required(ErrorMessage = "ادخل المحكمه")]

        public string CourtName { get; set; }
        [DisplayName("الدائره")]

        public string eldarea { get; set; }
        [DisplayName("القرار")]
        [Required(ErrorMessage = "ادخل القرار")]


        public string desicion { get; set; }
        [DisplayName("طلبات المحكمه")]
        [Required(ErrorMessage = "ادخل الطلبات")]

        public string orders { get; set; }
        public string EmpName { get; set; }


    }
}