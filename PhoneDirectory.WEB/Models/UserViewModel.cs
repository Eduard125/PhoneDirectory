using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class UserViewModel
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
        public string NameStrucDiv { get; set; }
        public int PostId { get; set; }
        public string NamePost { get; set; }
        public string Email { get; set; }
        public string PersonalNum { get; set; }
        public string PersonalNum1 { get; set; }       
    }
}
