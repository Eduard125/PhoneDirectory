using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class StructuralDivisionViewModel
    {
        public int Id { get; set; }       
        public int StrucDivId { get; set; }
        public string NameStrucDiv { get; set; }
        public int PostId { get; set; }
        public string NamePost { get; set; }
        //public int DepartmentNumberId { get; set; }
        public string NameDepartmentNumber { get; set; }
       // public int DepartmentNumber1Id { get; set; }
        public string NameDepartmentNumber1 { get; set; }
       // public int DepartmentMobNumberId { get; set; }
        public string NameDepartmentMobNumber { get; set; }
        //public int DepartmentMobNumber1Id { get; set; }
        public string NameDepartmentMobNumber1 { get; set; }

        public string Title { get; set; } 
    }
}
