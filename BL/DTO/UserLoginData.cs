using System;

namespace BL.DTO
{
    public class UserLoginData
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
