using System;
using Register.DOMAIN.Common;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.DTO;

public class JobDto : Basic
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid? DeptId { get; set; }
    public DepartMent? Dept { get; set; }
}
