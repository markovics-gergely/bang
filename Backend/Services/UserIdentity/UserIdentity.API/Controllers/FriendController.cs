using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendViewModel>>> GetFriendsAsync(CancellationToken cancellationToken)
        {
            var query = new GetFriendsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("unaccepted")]
        public async Task<ActionResult<IEnumerable<FriendViewModel>>> GetUnacceptedFriendsAsync(CancellationToken cancellationToken)
        {
            var query = new GetUnacceptedFriendsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> AddFriendAsync(string name, CancellationToken cancellationToken)
        {
            var command = new CreateFriendCommand(name);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteFriendAsync(string name, CancellationToken cancellationToken)
        {
            var command = new DeleteFriendCommand(name);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

    }
}
