using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string FullName { get; set; }
        public int StrucDivId { get; set; }
        public int PostId { get; set; }        
        public string Email { get; set; }
    }
}
