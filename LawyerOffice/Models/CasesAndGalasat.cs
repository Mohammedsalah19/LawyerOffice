using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class CasesAndGalasat
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<elgalasat> galasatX { get; set; }
        public IEnumerable<Cases> CasesX { get; set; }
        public IEnumerable<Client> clientX { get; set; }
 

        public IEnumerable<Courts> courtsX { get; set; }
    }
}