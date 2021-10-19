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

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController
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

        [HttpPut("decrement-health")]
        public async Task<ActionResult> DecrementPlayerHealthAsync(CancellationToken cancellationToken)
        {
            var command = new DecrementPlayerHealthCommand();

            await _mediator.Send(command, cancellationToken);

            foreach (var users in GameHub.Connections)
            {
                var query = new GetGameBoardByUserIdQuery(users.Value);
                var gameboard = await _mediator.Send(query, cancellationToken);
                await _hub.Clients.Client(users.Key).RefreshBoard(gameboard);
            }

            return new NoContentResult();
        }

        [HttpGet("permissions")]
        public async Task<ActionResult<PermissionViewModel>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            var query = new GetPermissionsQuery();

            return await _mediator.Send(query, cancellationToken);
        }
    }
}
