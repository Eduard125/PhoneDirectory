using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class UserEditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [StringLength(34, MinimumLength = 3, ErrorMessage = "От 3 до 34 символов")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(24, MinimumLength = 3, ErrorMessage = "От 3 до 24 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите отчество")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "От 3 до 40 символов")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Структурное подразделение")]
        [Display(Name = "Отдел")]
        public int StrucDivId { get; set; }


        [Required(ErrorMessage = "Должность")]
        [Display(Name = "Должность")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Личный номер")]
        [StringLength(50)]
        [RegularExpression("[+]{1}375-[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Введите телефон в формате +375-XX-XXX-XXXX")]
        [Display(Name = "Личный номер")]
        public string PersonalNum { get; set; }

        [Required(ErrorMessage = "Личный номер2")]
        [StringLength(50)]
        [Display(Name = "Личный номер2")]
        public string PersonalNum1 { get; set; }       

        [Required(ErrorMessage = "Укажите E-mail")]
        [MaxLength(100, ErrorMessage = "Не более 100 символов")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Укажите корректный E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
