using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.DTO
{
    public class PersonalNumberDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PersonalNum { get; set; }
    }
}
