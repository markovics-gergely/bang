using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Bang.DAL.Domain.Constants.Enums;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Application.Commands.Commands;
using Microsoft.AspNetCore.Authorization;
using Bang.BLL.Application.Commands.DataTransferObjects;
using Microsoft.AspNetCore.SignalR;
using Bang.API.SignalR;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub, IGameHubClient> _hub;

        public CardController(IMediator mediator, IHubContext<GameHub, IGameHubClient> hub)
        {
            _mediator = mediator;
            _hub = hub;
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<CardViewModel>> GetCardByTypeAsync(CardType type, CancellationToken cancellationToken)
        {
            var query = new GetCardByTypeQuery(type);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardViewModel>>> GetCardsAsync(CancellationToken cancellationToken)
        {
            var query = new GetCardsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPut("play-card")]
        public async Task<ActionResult> PlayCardAsync([FromBody] PlayCardDto playCardDto, CancellationToken cancellationToken)
        {
            var command = new PlayCardCommand(playCardDto);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("discard-card/{playerCardId}")]
        public async Task<ActionResult> DiscardCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            var command = new DiscardCardCommand(playerCardId);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("draw-card/{count}")]
        public async Task<ActionResult> DrawCardAsync(int count, CancellationToken cancellationToken)
        {
            var command = new DrawCardCommand(count);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("draw-a-card/{id}")]
        public async Task<ActionResult> DrawCardByIdAsync(long id, CancellationToken cancellationToken)
        {
            var command = new DrawCardByIdCommand(id);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpPut("draw-a-card-from-hand/{playerId}")]
        public async Task<ActionResult> DrawCardFromHandAsync(long playerId, CancellationToken cancellationToken)
        {
            var command = new DrawCardFromPlayerCommand(playerId);

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }
    }
}
