using UserIdentity.BLL.Application.Commands.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.Friend.ViewModels;
using UserIdentity.BLL.Infrastructure.Queries.Friend.Queries;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
            var command = new GetFriendsQuery();

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet("unaccapted")]
        public async Task<ActionResult<IEnumerable<FriendViewModel>>> GetUnacceptedFriendsAsync(CancellationToken cancellationToken)
        {
            var command = new GetUnacceptedFriendsQuery();

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriendAsync(string id, CancellationToken cancellationToken)
        {
            var command = new AddFriendCommand(id);

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFriendAsync(string id, CancellationToken cancellationToken)
        {
            var command = new AddFriendCommand(id);

            return await _mediator.Send(command, cancellationToken);
        }

    }
}
