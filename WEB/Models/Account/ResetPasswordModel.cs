using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models.Account
{
    public class ResetPasswordModel
    {
        public Guid PasswordResetToken { get; set; }

        public string NewPassword { get; set; }
    }
}