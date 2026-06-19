using System;
using System.Collections.Generic;
using System.Text;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class UserRegister:Basic
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }




    }
}
