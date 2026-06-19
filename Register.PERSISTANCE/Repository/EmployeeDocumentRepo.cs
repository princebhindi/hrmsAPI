using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class EmployeeDocumentRepo : IEmployeeDocument
    {
        private readonly ApllicationDbContext _context;

        public EmployeeDocumentRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDocument>> GetAllEmployeeDocumentsAsync(int pageNumber, int pageSize)
        {
            return await _context.EmployeeDocuments
                .Include(ed => ed.Employee)
                .Where(ed => ed.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<EmployeeDocument?> GetEmployeeDocumentByIdAsync(Guid id)
        {
            return await _context.EmployeeDocuments
                .Include(ed => ed.Employee)
                .FirstOrDefaultAsync(ed => ed.Id == id && ed.IsActive);
        }

        public async Task<EmployeeDocument> AddEmployeeDocumentAsync(EmployeeDocument employeeDocument)
        {
            await _context.EmployeeDocuments.AddAsync(employeeDocument);
            await _context.SaveChangesAsync();
            return employeeDocument;
        }

        public async Task<bool> UpdateEmployeeDocumentAsync(EmployeeDocument employeeDocument)
        {
            var existing = await _context.EmployeeDocuments.FindAsync(employeeDocument.Id);
            if (existing == null) return false;

            existing.EmpId = employeeDocument.EmpId;
            existing.DocumentType = employeeDocument.DocumentType;
            existing.DocumentName = employeeDocument.DocumentName;
            existing.DocumentPath = employeeDocument.DocumentPath;
            existing.OnUpdate = DateTime.Now;

            _context.EmployeeDocuments.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeDocumentAsync(Guid id)
        {
            var document = await _context.EmployeeDocuments.FindAsync(id);
            if (document == null) return false;

            document.IsActive = false;
            document.OnUpdate = DateTime.Now;
            _context.EmployeeDocuments.Update(document);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetEmployeeDocumentCountAsync()
        {
            return await _context.EmployeeDocuments.CountAsync();
        }
    }
}
