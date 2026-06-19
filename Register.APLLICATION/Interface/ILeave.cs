using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface ILeave
    {
        Task<IEnumerable<Leave>> GetAllLeavesAsync(int pageNumber, int pageSize);
        Task<Leave?> GetLeaveByIdAsync(Guid id);
        Task<Leave> AddLeaveAsync(Leave leave);
        Task<bool> UpdateLeaveAsync(Leave leave);
        Task<bool> DeleteLeaveAsync(Guid id);
        Task<int> GetLeaveCountAsync();
    }
}
