using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Enviroment
{
    public class UserData
    {
        public Guid Id { get; set; }

        public string Role { get; set; }

        public UserData() { }

        public UserData(Guid id, string role)
        {
            Id = id;
            Role = role;
        }
    }
}