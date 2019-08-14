using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class CasesAndClients
    {
        [Key]
        public int id { get; set; }

        public IEnumerable<Cases> Cases { get; set; }
        public Client Client { get; set; }
    }
}