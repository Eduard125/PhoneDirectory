using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Login { get; set; }        
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string PersonalNum { get; set; }
        public string PersonalNum1 { get; set; }
        public string Surname { get; set; }        
        public string Name { get; set; }        
        public string Patronymic { get; set; }       
        public int StrucDivId { get; set; }
        public StructuralDivision StructuralDivision { get; set; }
        public DepartmentNumber DepartmentNumber { get; set; }
        public DepartmentMobNumber DepartmentMobNumber { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Email { get; set; }

        public ICollection<DivisionPost> DivisionPosts { get; set; }
        //public ICollection<StructuralDivision> StructuralDivisions { get; set; }
        //public ICollection<DepartmentNumber> DepartmentNumbers { get; set; }

    }
}
