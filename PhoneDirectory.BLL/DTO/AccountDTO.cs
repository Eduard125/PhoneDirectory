using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } 
        public string RoleDesign { get; set; }
    }
}
