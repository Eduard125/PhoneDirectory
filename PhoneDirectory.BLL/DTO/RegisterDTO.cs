using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class RegisterDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int StrucDivId { get; set; }
        public int PostId { get; set; }
        public string PersonalNum { get; set; }
        public string StrucDivNum { get; set; }
        public string StrucDivMobNum { get; set; }
        public string Email { get; set; }
    }
}
