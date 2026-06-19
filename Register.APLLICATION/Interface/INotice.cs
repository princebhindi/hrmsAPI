using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface INotice
    {
        Task<IEnumerable<Notice>> GetAllNoticesAsync(int pageNumber, int pageSize);
        Task<Notice?> GetNoticeByIdAsync(Guid id);
        Task<Notice> AddNoticeAsync(Notice notice);
        Task<bool> UpdateNoticeAsync(Notice notice);
        Task<bool> DeleteNoticeAsync(Guid id);
        Task<int> GetNoticeCountAsync();
    }
}
