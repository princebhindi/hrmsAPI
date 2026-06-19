using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteAttendanceHandler : IRequestHandler<DeleteAttendanceCommand, APIResponse<bool>>
    {
        private readonly IAttendance _repo;

        public DeleteAttendanceHandler(IAttendance repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteAttendanceAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Attendance deleted successfully" : "Attendance record not found",
                Data = result
            };
        }
    }
}
