using System;
using System.Collections.Generic;
using System.Text;

namespace Register.APPLICATION.DTO
{
    public class AuthResponseDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
