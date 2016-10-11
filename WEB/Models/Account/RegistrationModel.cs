using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Account
{
    public class RegistrationModel
    {
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [Display(Name = "Электронный адрес")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Некорректный email адрес")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}