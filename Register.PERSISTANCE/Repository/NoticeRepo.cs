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
    public class NoticeRepo : INotice
    {
        private readonly ApllicationDbContext _context;

        public NoticeRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notice>> GetAllNoticesAsync(int pageNumber, int pageSize)
        {
            return await _context.Notices
                .Include(n => n.TargetDepartment)
                .Where(n => n.IsActive)
                .OrderByDescending(n => n.OnCreate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Notice?> GetNoticeByIdAsync(Guid id)
        {
            return await _context.Notices
                .Include(n => n.TargetDepartment)
                .FirstOrDefaultAsync(n => n.Id == id && n.IsActive);
        }

        public async Task<Notice> AddNoticeAsync(Notice notice)
        {
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();
            return notice;
        }

        public async Task<bool> UpdateNoticeAsync(Notice notice)
        {
            var existing = await _context.Notices.FindAsync(notice.Id);
            if (existing == null) return false;

            existing.Title = notice.Title;
            existing.Content = notice.Content;
            existing.TargetDepartmentId = notice.TargetDepartmentId;
            existing.ExpiryDate = notice.ExpiryDate;
            existing.OnUpdate = DateTime.Now;

            _context.Notices.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoticeAsync(Guid id)
        {
            var notice = await _context.Notices.FindAsync(id);
            if (notice == null) return false;

            notice.IsActive = false;
            notice.OnUpdate = DateTime.Now;
            _context.Notices.Update(notice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetNoticeCountAsync()
        {
            return await _context.Notices.CountAsync(n => n.IsActive);
        }
    }
}
