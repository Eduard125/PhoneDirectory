using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class StructuralDivisionCreateViewModel
    {
        public int Id { get; set; }      
        [Required(ErrorMessage = "Выберите отдел")]
        [Display(Name = "Отдел")]
        public int StrucDivId { get; set; }
        [Required(ErrorMessage = "Выберите имя отдела")]
        [Display(Name = "Имя отдела")]
        //public string NameStrucDiv { get; set; }
        //[Required(ErrorMessage = "Выберите должность")]
        //[Display(Name = "Должность")]
        //public int PostId { get; set; }        
        //[Required(ErrorMessage = "Выберите имя должности")]
        //[Display(Name = "Имя должности")]
        //public string NamePost { get; set; }
        //[Required(ErrorMessage = "Служебный номер")]
        //[Display(Name = "Служебный номер")]
        public string StrucDivNum { get; set; }       

        [Required(ErrorMessage = "Служебный номер1")]
        [Display(Name = "Служебный номер1")]
        public string StrucDivNum1 { get; set; }
    }
}
