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
    public class SalaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSalaries([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllSalariesQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSalaryCount()
        {
            var res = await _mediator.Send(new GetSalaryCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSalaryById(Guid id)
        {
            var res = await _mediator.Send(new GetSalaryByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSalary([FromBody] SalaryDto salaryDto)
        {
            var res = await _mediator.Send(new AddSalaryCommand(salaryDto));
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSalary([FromBody] SalaryDto salaryDto)
        {
            var res = await _mediator.Send(new UpdateSalaryCommand(salaryDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            var res = await _mediator.Send(new DeleteSalaryCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
