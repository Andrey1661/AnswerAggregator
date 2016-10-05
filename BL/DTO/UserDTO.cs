using System;

namespace BL.DTO
{
    // ReSharper disable once InconsistentNaming
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}
