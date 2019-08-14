    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class ClientWithCases
    {

        [Key]
        public int id { get; set; }
        public IEnumerable<Cases> CaseX { get; set; }
        public Client ClientX { get; set; }
        public IEnumerable< Courts >CourtX { get; set; }
        public IEnumerable<Employees> empX { get; set; }
        public IEnumerable<Client> ClientXs { get; set; }





    }
}