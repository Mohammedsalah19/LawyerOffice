using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models
{
    public class attachmentAndTypes
    {
        public IEnumerable<attachements> attX { get; set; }

        public IEnumerable<attachmentType> attTypeX { get; set; }
    }
}