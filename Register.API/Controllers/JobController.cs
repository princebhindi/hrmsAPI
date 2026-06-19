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
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllJobsQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("/GetCount")]
        public async Task<IActionResult> GetJobCount()
        {
            var res = await _mediator.Send(new GetJobCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var res = await _mediator.Send(new GetJobByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddJob([FromBody] JobDto jobDto)
        {
            var res = await _mediator.Send(new AddJobCommand(jobDto));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJob([FromBody] JobDto jobDto)
        {
            var res = await _mediator.Send(new UpdateJobCommand(jobDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var res = await _mediator.Send(new DeleteJobCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
