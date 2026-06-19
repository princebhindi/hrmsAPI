using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Register.DOMAIN.Common
{
    public class Basic
    {
        public Guid Id { get; set; }

        public DateTime OnCreate { get; set; } = DateTime.Now;

        public DateTime? OnUpdate { get; set; }

        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
