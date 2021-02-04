using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class StructuralDivisionsFilterDTO
    {
        public bool UseStructuralDivision { get; set; }
        public int StrucDivId { get; set; }      
        public bool UsePost { get; set; }
        public int PostId { get; set; }
    }
}
