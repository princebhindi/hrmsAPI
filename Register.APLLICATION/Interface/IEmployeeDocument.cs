using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IEmployeeDocument
    {
        Task<IEnumerable<EmployeeDocument>> GetAllEmployeeDocumentsAsync(int pageNumber, int pageSize);
        Task<EmployeeDocument?> GetEmployeeDocumentByIdAsync(Guid id);
        Task<EmployeeDocument> AddEmployeeDocumentAsync(EmployeeDocument employeeDocument);
        Task<bool> UpdateEmployeeDocumentAsync(EmployeeDocument employeeDocument);
        Task<bool> DeleteEmployeeDocumentAsync(Guid id);
        Task<int> GetEmployeeDocumentCountAsync();
    }
}
