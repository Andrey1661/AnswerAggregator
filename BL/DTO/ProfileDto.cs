﻿using System;

namespace BL.DTO
{
    // ReSharper disable once InconsistentNaming
    public class ProfileDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string University { get; set; }

        public string Institute { get; set; }

        public Guid GroupId { get; set; }

        public string Group { get; set; }

        public bool AccountVerified { get; set; }
    }
}
