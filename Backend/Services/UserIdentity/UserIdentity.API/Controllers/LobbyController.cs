using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Commands.DataTransferObjects;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LobbyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LobbyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("actual-lobby")]
        public async Task<ActionResult<LobbyViewModel>> GetActualLobbyAsync(CancellationToken cancellationToken)
        {
            var query = new GetActualLobbyQuery();

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<LobbyAccountViewModel>>> GetLobbyAccountsAsync(long id, CancellationToken cancellationToken)
        {
            var query = new GetLobbyAccountsQuery(id);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateLobbyAsync(CancellationToken cancellationToken)
        {
            var command = new CreateLobbyCommand();

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("{id}/start-game")]
        public async Task<IActionResult> CreateGameBoardAsync(int id, [FromBody] GameBoardDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateGameBoardCommand(id, dto);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("connect/{password}")]
        public async Task<IActionResult> CreateLobbyAccountAsync(string password, CancellationToken cancellationToken)
        {
            var command = new CreateLobbyAccountCommand(password);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut("invite/{friendName}")]
        public async Task<IActionResult> UpdateLobbyInviteTrueAsync(string friendName, CancellationToken cancellationToken)
        {
            var command = new UpdateLobbyInviteTrueCommand(friendName);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut("accept-invite/{friendName}")]
        public async Task<IActionResult> UpdateLobbyInviteFalseAsync(string friendName, CancellationToken cancellationToken)
        {
            var command = new UpdateLobbyInviteFalseCommand(friendName);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}/kick/{name}")]
        public async Task<IActionResult> DeleteLobbyAccountByOwnerAsync(long id, string name, CancellationToken cancellationToken)
        {
            var command = new DeleteLobbyAccountByOwnerCommand(id, name);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}/disconnect")]
        public async Task<IActionResult> DeleteLobbyAccountAsync(long id, CancellationToken cancellationToken)
        {
            var command = new DeleteLobbyAccountCommand(id);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
