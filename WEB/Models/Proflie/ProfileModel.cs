using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEB.Models.Topic;

namespace WEB.Models.Proflie
{
    public class ProfileModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Электронный адрес")]
        public string Email { get; set; }

        public string AvatarLink { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public bool AccountVerified { get; set; }

        public IEnumerable<SubjectModel> Subjects { get; set; }  
    }
}