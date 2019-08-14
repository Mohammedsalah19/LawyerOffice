using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models.DAL
{
    public class IEClinetsWithIECase
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Cases> CaseX { get; set; }
        public IEnumerable<Client> ClientX { get; set; }

    }

}