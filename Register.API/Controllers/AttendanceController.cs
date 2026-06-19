using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Queries;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAttendance([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllAttendanceQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAttendanceCount()
        {
            var res = await _mediator.Send(new GetAttendanceCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(Guid id)
        {
            var res = await _mediator.Send(new GetAttendanceByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceDto attendanceDto)
        {
            var res = await _mediator.Send(new AddAttendanceCommand(attendanceDto));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceDto attendanceDto)
        {
            var res = await _mediator.Send(new UpdateAttendanceCommand(attendanceDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            var res = await _mediator.Send(new DeleteAttendanceCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
