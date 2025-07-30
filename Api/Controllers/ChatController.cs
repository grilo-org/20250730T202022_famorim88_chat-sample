using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IMediator mediator) => _mediator = mediator;

        [HttpPost("message")]
        public async Task<IActionResult> PostMessage([FromBody] CreateMessageCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }

}
