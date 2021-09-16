using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Application.Commands.Commands;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPut("{id}/decrement-health")]
        public async Task<ActionResult> DecrementPlayerHealthAsync(long id, CancellationToken cancellationToken)
        {
            var command = new DecrementPlayerHealthCommand(id);

            await _mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }
    }
}
