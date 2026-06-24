using System;
using System.Collections.Generic;
using System.Text;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class UserRegisterDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
