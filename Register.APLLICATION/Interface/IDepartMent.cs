using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IDepartMent
    {
        Task<IEnumerable<DepartMent>> GetAllDepartmentsAsync(int pageNumber, int pageSize);
        Task<DepartMent?> GetDepartmentByIdAsync(Guid id);
        Task<DepartMent> AddDepartmentAsync(DepartMent department);
        Task<bool> UpdateDepartmentAsync(DepartMent department);
        Task<bool> DeleteDepartmentAsync(Guid id);
    }
}
