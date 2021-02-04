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
        public PersonalNumber PersonalNumber { get; set; }
        [StringLength(100)]
        public string Login { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Patronymic { get; set; }       
        public int StrucDivId { get; set; }
        public StructuralDivision StructuralDivision { get; set; }
        public DepartmentNumber DepartmentNumber { get; set; }
        public DepartmentMobNumber DepartmentMobNumber { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
    }
}
