using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class StructuralDivisionUsersViewModel
    {       
        public StructuralDivisionViewModel StructuralDivision { get; set; }           
        public List<UserViewModel> Users { get; set; }        
    }
}
