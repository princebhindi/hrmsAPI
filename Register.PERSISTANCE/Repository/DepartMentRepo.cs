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
    public class DepartMentRepo : IDepartMent
    {
        private readonly ApllicationDbContext _context;

        public DepartMentRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartMent>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            return await _context.DepartMents
                .Where(d => d.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<DepartMent?> GetDepartmentByIdAsync(Guid id)
        {
            return await _context.DepartMents
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<DepartMent> AddDepartmentAsync(DepartMent department)
        {
            await _context.DepartMents.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartmentAsync(DepartMent department)
        {
            var existing = await _context.DepartMents.FindAsync(department.Id);
            if (existing == null || !existing.IsActive) return false;

            existing.Name = department.Name;
            existing.OnUpdate = DateTime.Now;

            _context.DepartMents.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(Guid id)
        {
            var department = await _context.DepartMents.FindAsync(id);
            if (department == null || !department.IsActive) return false;

            department.IsActive = false;
            department.OnUpdate = DateTime.Now;

            _context.DepartMents.Update(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
