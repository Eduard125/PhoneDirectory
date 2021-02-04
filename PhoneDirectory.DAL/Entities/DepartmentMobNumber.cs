using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class DepartmentMobNumber
    {
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public string StrucDivMobNum { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
