using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Interface
{
    public interface IAttendance
    {
        Task<IEnumerable<Attendance>> GetAllAttendanceAsync(int pageNumber, int pageSize);
        Task<Attendance?> GetAttendanceByIdAsync(Guid id);
        Task<Attendance> AddAttendanceAsync(Attendance attendance);
        Task<bool> UpdateAttendanceAsync(Attendance attendance);
        Task<bool> DeleteAttendanceAsync(Guid id);
        Task<int> GetAttendanceCountAsync();
    }
}
