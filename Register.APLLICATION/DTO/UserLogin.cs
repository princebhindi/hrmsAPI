using System;
using System.Collections.Generic;
using System.Text;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class UserLogin:Basic
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
