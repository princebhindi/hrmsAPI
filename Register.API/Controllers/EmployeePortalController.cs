using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.APPLICATION.Queries;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePortalController : ControllerBase
    {
        private readonly IMediator mediator;
        public EmployeePortalController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDashBoard([FromRoute] Guid id)
        {
            var res = await mediator.Send(new GetEmployeePortalDataQueries(id));
            return Ok(res);
        }
    }
}
