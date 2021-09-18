using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MediatR;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LobbyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<LobbyAccountViewModel>>> GetLobbyAccountsAsync(long lobbyId, CancellationToken cancellationToken)
        {
            var query = new GetLobbyAccountsQuery(lobbyId);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost("connect/{password}")]
        public async Task<IActionResult> CreateLobbyAccountAsync(string password, CancellationToken cancellationToken)
        {
            var command = new CreateLobbyAccountCommand(password);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("disconnect")]
        public async Task<IActionResult> DeleteLobbyAccountAsync(CancellationToken cancellationToken)
        {
            var command = new DeleteLobbyAccountCommand();

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateLobbyAsync(CancellationToken cancellationToken)
        {
            var command = new CreateLobbyCommand();

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLobbyAsync(CancellationToken cancellationToken)
        {
            var command = new CreateLobbyCommand();

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
