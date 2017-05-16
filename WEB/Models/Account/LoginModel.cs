using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Необходимо указать имя пользователя или Email")]
        [Display(Name = "Имя пользователя или Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Оставаться в системе")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}