using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class attachmentType
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("النوع")]
        public string AttType { get; set; }

    }
}