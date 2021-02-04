using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class DepartmentMobNumber
    {
        [Key]
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public string StrucDivMobNum { get; set; }
        public string StrucDivMobNum1 { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<DivisionPost> DivisionPosts { get; set; }
    }
}
