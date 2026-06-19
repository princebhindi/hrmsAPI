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
    public class NoticesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoticesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotices([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetAllNoticesQuery(pageNumber, pageSize));
            return Ok(res);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetNoticeCount()
        {
            var res = await _mediator.Send(new GetNoticeCountQuery());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticeById(Guid id)
        {
            var res = await _mediator.Send(new GetNoticeByIdQuery(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNotice([FromBody] NoticeDto noticeDto)
        {
            var res = await _mediator.Send(new AddNoticeCommand(noticeDto));
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNotice([FromBody] NoticeDto noticeDto)
        {
            var res = await _mediator.Send(new UpdateNoticeCommand(noticeDto));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNotice(Guid id)
        {
            var res = await _mediator.Send(new DeleteNoticeCommand(id));
            if (!res.Sucess) return NotFound(res);
            return Ok(res);
        }
    }
}
