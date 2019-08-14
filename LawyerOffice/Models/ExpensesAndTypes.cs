using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class ExpensesAndTypes
    {
        [Key]
        public int id { get; set; }

        public IEnumerable<ExpensesModel> expenX { get; set; }
        public IEnumerable<Expensestypes> expenTypeX { get; set; }
        public IEnumerable<Offices> officeX { get; set; }

    }
}