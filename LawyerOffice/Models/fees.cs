using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class fees
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("رقم القضيه")]

        public int NumberOfCase { get; set; }
        [DisplayName("رقم الموكل")]

        public Client clientID { get; set; }
        //[DisplayName("مجكل الاتعاب")]

        //public int totalFees { get; set; }
        //[DisplayName("باقى من الاتعاب")]

        //public int finallFees { get; set; }
        [DisplayName("دفعه جديده")]

        public int recived { get; set; }
        [DisplayName("تاريخ الدفعه")]

        public DateTime DateofRevice { get; set; }
        [DisplayName("ملاحظات")]

        public string notes { get; set; }
        [DisplayName("مستلم الدفعه")]

        public string EmployeeName { get; set; }

    }
}