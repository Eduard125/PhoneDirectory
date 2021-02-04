using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class DepartmentNumber
    {
        [Key]
        public int Id { get; set; }
        public int StrucDivId { get; set; }
        public string StrucDivNum { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
