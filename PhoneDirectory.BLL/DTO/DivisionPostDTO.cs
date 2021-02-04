using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class DivisionPostDTO
    {
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public StructuralDivision StructuralDivision { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string StrucDivNum { get; set; }
        public string StrucDivNum1 { get; set; }
    }
}
