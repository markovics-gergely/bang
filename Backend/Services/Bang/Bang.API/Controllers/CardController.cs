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
using Bang.DAL.Domain.Constants;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CardController
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

            foreach (var users in GameHub.Connections)
            {
                var query = new GetGameBoardByUserIdQuery(users.Value);
                var gameboard = await _mediator.Send(query, cancellationToken);
                await _hub.Clients.Client(users.Key).RefreshBoard(gameboard);
            }

            return new NoContentResult();
        }

        [HttpPut("discard-card/{playerCardId}")]
        public async Task<ActionResult> DiscardCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            var command = new DiscardCardCommand(playerCardId);

            await _mediator.Send(command, cancellationToken);

            foreach (var users in GameHub.Connections)
            {
                var query = new GetGameBoardByUserIdQuery(users.Value);
                var gameboard = await _mediator.Send(query, cancellationToken);
                await _hub.Clients.Client(users.Key).RefreshBoard(gameboard);
            }

            return new NoContentResult();
        }

        [HttpPut("draw-card/{count}")]
        public async Task<ActionResult> DrawCardAsync(int count, CancellationToken cancellationToken)
        {
            var command = new DrawCardCommand(count);

            await _mediator.Send(command, cancellationToken);

            foreach (var users in GameHub.Connections)
            {
                var query = new GetGameBoardByUserIdQuery(users.Value);
                var gameboard = await _mediator.Send(query, cancellationToken);
                await _hub.Clients.Client(users.Key).RefreshBoard(gameboard);
            }

            return new NoContentResult();
        }
    }
}
