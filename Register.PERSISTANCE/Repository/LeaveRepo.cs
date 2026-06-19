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
    public class LeaveRepo : ILeave
    {
        private readonly ApllicationDbContext _context;

        public LeaveRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Leave>> GetAllLeavesAsync(int pageNumber, int pageSize)
        {
            return await _context.Leaves
                .Include(l => l.Employee)
                .Include(l => l.User)
                .Where(l => l.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Leave?> GetLeaveByIdAsync(Guid id)
        {
            return await _context.Leaves
                .Include(l => l.Employee)
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
        }

        public async Task<Leave> AddLeaveAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<bool> UpdateLeaveAsync(Leave leave)
        {
            var existing = await _context.Leaves.FindAsync(leave.Id);
            if (existing == null) return false;

            existing.EmpId = leave.EmpId;
            existing.UserId = leave.UserId;
            existing.IsPending = leave.IsPending;
            existing.IsRejected = leave.IsRejected;
            existing.IsApproved = leave.IsApproved;
            existing.StartDate = leave.StartDate;
            existing.EndDate = leave.EndDate;
            existing.Reason = leave.Reason;
            existing.OnUpdate = DateTime.Now;

            _context.Leaves.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLeaveAsync(Guid id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null) return false;

            leave.IsActive = false;
            leave.OnUpdate = DateTime.Now;
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetLeaveCountAsync()
        {
            return await _context.Leaves.CountAsync();
        }
    }
}
