using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
        Task<int> GetEmployeeCountAsync();
    }
}