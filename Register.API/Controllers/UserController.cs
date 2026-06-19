using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Queries;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto user)
        {
            var res = await mediator.Send(new RegisterUserCommand(user));
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin user)
        {
            var res = await mediator.Send(new LoginUserCommand(user));
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await mediator.Send(new GetAllUsersQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var res = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegisterDto user)
        {
            var res = await mediator.Send(new UpdateUserCommand(user));
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var res = await mediator.Send(new DeleteUserCommand(id));
            return Ok(res);
        }
    }
}
