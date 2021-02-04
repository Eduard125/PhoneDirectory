using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class StructuralDivisionDTO
    {
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public string NameStrucDiv { get; set; }
        public int PostId { get; set; }
        public string NamePost { get; set; }
        public string NameDepartmentNumber { get; set; }
        public string NameDepartmentNumber1 { get; set; }

        public string NameDepartmentMobNumber { get; set; }
        public string NameDepartmentMobNumber1 { get; set; }
        public string Title { get; set; }
    }
}
