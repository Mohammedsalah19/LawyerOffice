using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class Logs
    {
        [Key]
        public int id { get; set; }

        public string username { get; set; }

        public DateTime date { get; set; }
        public string action { get; set; }
        public string ipaddress { get; set; }

    }
}