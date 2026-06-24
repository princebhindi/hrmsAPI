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
    public class LeavesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeavesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllLeaves([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllLeavesQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLeaveCount()
        {
            var res = await _mediator.Send(new GetLeaveCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLeaveById(Guid id)
        {
            var res = await _mediator.Send(new GetLeaveByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddLeave([FromBody] LeavesDto leaveDto)
        {
            var res = await _mediator.Send(new AddLeaveCommand(leaveDto));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeave([FromBody] LeavesDto leaveDto)
        {
            var res = await _mediator.Send(new UpdateLeaveCommand(leaveDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(Guid id)
        {
            var res = await _mediator.Send(new DeleteLeaveCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
