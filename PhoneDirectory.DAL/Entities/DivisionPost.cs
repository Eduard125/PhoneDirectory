using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class DivisionPost
    {
        [Key]
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public StructuralDivision StructuralDivision { get; set; }
        public DepartmentNumber DepartmentNumber { get; set; }
        public DepartmentMobNumber DepartmentMobNumber { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
       
    }
}
