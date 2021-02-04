using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class PersonalNumber
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PersonalNum { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
