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
    public class AttendanceRepo : IAttendance
    {
        private readonly ApllicationDbContext _context;

        public AttendanceRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendanceAsync(int pageNumber, int pageSize)
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .Where(a => a.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(Guid id)
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
        }

        public async Task<Attendance> AddAttendanceAsync(Attendance attendance)
        {
            if (attendance.CheckInTime.HasValue && attendance.CheckOutTime.HasValue)
            {
                var duration = attendance.CheckOutTime.Value - attendance.CheckInTime.Value;
                attendance.TotalHours = Math.Round(duration.TotalHours, 2);
            }
            else
            {
                attendance.TotalHours = null;
            }

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<bool> UpdateAttendanceAsync(Attendance attendance)
        {
            var existing = await _context.Attendances.FindAsync(attendance.Id);
            if (existing == null) return false;

            existing.EmpId = attendance.EmpId;
            existing.Date = attendance.Date;
            existing.CheckInTime = attendance.CheckInTime;
            existing.CheckOutTime = attendance.CheckOutTime;
            
            if (attendance.CheckInTime.HasValue && attendance.CheckOutTime.HasValue)
            {
                var duration = attendance.CheckOutTime.Value - attendance.CheckInTime.Value;
                existing.TotalHours = Math.Round(duration.TotalHours, 2);
            }
            else
            {
                existing.TotalHours = null;
            }

            existing.Status = attendance.Status;
            existing.OnUpdate = DateTime.Now;

            _context.Attendances.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAttendanceAsync(Guid id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return false;

            attendance.IsActive = false;
            attendance.OnUpdate = DateTime.Now;
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetAttendanceCountAsync()
        {
            return await _context.Attendances.CountAsync();
        }
    }
}
