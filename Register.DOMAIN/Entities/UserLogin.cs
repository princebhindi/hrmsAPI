using System;
using System.Collections.Generic;
using System.Text;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class UserLogin:Basic
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
