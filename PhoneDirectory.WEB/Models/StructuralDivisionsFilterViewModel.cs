using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class StructuralDivisionsFilterViewModel
    {     
        public bool UseStructuralDivision { get; set; }
        public int StrucDivId { get; set; }              
        public bool UsePost { get; set; }
        public int PostId { get; set; }     
       
    }
}
