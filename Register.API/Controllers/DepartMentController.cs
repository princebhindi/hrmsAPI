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
    public class DepartMentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartMentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDepartments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllDepartMentsQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            var res = await _mediator.Send(new GetDepartMentByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartMentDto departMentDto)
        {
            var res = await _mediator.Send(new AddDepartMentCommand(departMentDto));
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartMentDto departMentDto)
        {
            var res = await _mediator.Send(new UpdateDepartMentCommand(departMentDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var res = await _mediator.Send(new DeleteDepartMentCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
