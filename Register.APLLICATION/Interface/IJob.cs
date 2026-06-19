using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IJob
    {
        Task<IEnumerable<Job>> GetAllJobsAsync(int pageNumber, int pageSize);
        Task<Job?> GetJobByIdAsync(Guid id);
        Task<Job> AddJobAsync(Job job);
        Task<bool> UpdateJobAsync(Job job);
        Task<bool> DeleteJobAsync(Guid id);
        Task<int> GetJobCountAsync();
    }
}
