using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Application.Commands.Commands;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Bang.API.SignalR;
using Bang.BLL.Application.Commands.Player.DataTransferObjects;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub, IGameHubClient> _hub;

        public PlayerController(IMediator mediator, IHubContext<GameHub, IGameHubClient> hub)
        {
            _mediator = mediator;
            _hub = hub;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerViewModel>> GetPlayerAsync(long id, CancellationToken cancellationToken)
        {
            var query = new GetPlayerQuery(id);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> GetPlayersAsync(CancellationToken cancellationToken)
        {
            var query = new GetPlayersQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("gameboard/{id}")]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> GetPlayersByGameBoardAsync(long id, CancellationToken cancellationToken)
        {
            var query = new GetPlayersByGameBoardQuery(id);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("{id}/targetable")]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> GetTargetablePlayersAsync(long id, CancellationToken cancellationToken)
        {
            var query = new GetTargetablePlayersQuery(id);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("{id}/targetable/{range}")]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> GetTargetablePlayersByRangeAsync(long id, int range, CancellationToken cancellationToken)
        {
            var query = new GetTargetablePlayersByRangeQuery(id, range);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPut("decrement-health")]
        public async Task<ActionResult> DecrementPlayerHealthAsync(CancellationToken cancellationToken)
        {
            var command = new DecrementPlayerHealthCommand();

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("gain-health-for-cards")]
        public async Task<ActionResult> GainHealthForCardsAsync([FromBody] IEnumerable<long> cards, CancellationToken cancellationToken)
        {
            var command = new GainHealthForCardsCommand(cards);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("use-character")]
        public async Task<ActionResult> UseCharacterAsync([FromBody] CharacterDto characterDto, CancellationToken cancellationToken)
        {
            var command = new UseCharacterCommand(characterDto);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpGet("permissions")]
        public async Task<ActionResult<PermissionViewModel>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            var query = new GetPermissionsQuery();

            return await _mediator.Send(query, cancellationToken);
        }
    }
}
