using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface ISalary
    {
        Task<IEnumerable<Salary>> GetAllSalariesAsync(int pageNumber, int pageSize);
        Task<Salary?> GetSalaryByIdAsync(Guid id);
        Task<Salary> AddSalaryAsync(Salary salary);
        Task<bool> UpdateSalaryAsync(Salary salary);
        Task<bool> DeleteSalaryAsync(Guid id);
        Task<int> GetSalaryCountAsync();
    }
}
