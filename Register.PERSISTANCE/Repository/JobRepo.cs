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
    public class JobRepo : IJob
    {
        private readonly ApllicationDbContext _context;

        public JobRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync(int pageNumber, int pageSize)
        {
            return await _context.Jobs
                .Include(j => j.Dept)
                .Where(j => j.Dept != null && j.Dept.IsActive == true)
                .Where(j => j.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Job?> GetJobByIdAsync(Guid id)
        {
            return await _context.Jobs
                .Include(j => j.Dept)
                .FirstOrDefaultAsync(j => j.Id == id && j.IsActive);
        }

        public async Task<Job> AddJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<bool> UpdateJobAsync(Job job)
        {
            var existing = await _context.Jobs.FindAsync(job.Id);
            if (existing == null || !existing.IsActive) return false;

            existing.Title = job.Title;
            existing.Description = job.Description;
            existing.DeptId = job.DeptId;
            existing.OnUpdate = DateTime.Now;

            _context.Jobs.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null || !job.IsActive) return false;

            job.IsActive = false;
            job.OnUpdate = DateTime.Now;

            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetJobCountAsync()
        {
            var count = await _context.Jobs.CountAsync(j => j.IsActive);
            return count;
        }
    }
}
