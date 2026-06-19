using Register.DOMAIN.Common;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.DTO;

public class EmployeeDto : Basic
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public Guid? DeptId { get; set; }
    public DepartMent? Dept { get; set; }
}
