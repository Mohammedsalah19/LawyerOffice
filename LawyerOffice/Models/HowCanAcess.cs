using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class HowCanAcess
    {

        [Key]
        public int ID { get; set; }

 
        public int employeeID { get; set; }


        public bool Agenda { get; set; }
        public bool Cases { get; set; }
        public bool Employee { get; set; }
        public bool Clients { get; set; }
        public bool Staticis { get; set; }

        //public bool money { get; set; }
        public bool Setting { get; set; }
        public bool library { get; set; }


        public bool AccessAll { get; set; }






    }
}