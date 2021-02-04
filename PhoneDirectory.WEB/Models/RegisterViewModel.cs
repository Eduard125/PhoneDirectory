using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "От 3 до 20 символов")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите пароль еще раз")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "От 3 до 34 символов")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "От 3 до 24 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите отчество")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "От 3 до 40 символов")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Структурное подразделение")]
        [Display(Name = "Отдел")]
        public int StrucDivId { get; set; }


        [Required(ErrorMessage = "Должность")]
        [Display(Name = "Должность")]
        public int PostId { get; set; }


        [Required(ErrorMessage = "Личный номер")]
        [StringLength(100)]
        [RegularExpression("[+]{1}375-[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Введите телефон в формате +375-XX-XXX-XXXX")]
        [Display(Name = "Телефон")]
        public string PersonalNum { get; set; }

        [Required(ErrorMessage = "Служебный номер")]
        [StringLength(100)]
        [RegularExpression("[0-9]{6}", ErrorMessage = "Введите телефон в формате XXXXXX")]
        [Display(Name = "Телефон")]
        public string StrucDivNum { get; set; }

        [Required(ErrorMessage = "Служебный моб номер")]
        [StringLength(100)]
        [RegularExpression("[+]{1}375-[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Введите телефон в формате +375-XX-XXX-XXXX")]
        [Display(Name = "Телефон")]
        public string StrucDivMobNum { get; set; }

        [Required(ErrorMessage = "Укажите E-mail")]
        [MaxLength(100, ErrorMessage = "Не более 100 символов")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Укажите корректный E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
