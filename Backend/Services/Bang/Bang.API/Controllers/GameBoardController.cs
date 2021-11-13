using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.BLL.Application.Commands.Commands;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Bang.API.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GameBoardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub, IGameHubClient> _hub;

        public GameBoardController(IMediator mediator, IHubContext<GameHub, IGameHubClient> hub)
        {
            _mediator = mediator;
            _hub = hub;
        }

        [HttpGet("user")]
        public async Task<ActionResult<GameBoardByUserViewModel>> GetGameBoardByUserAsync(CancellationToken cancellationToken)
        {
            var query = new GetGameBoardByUserQuery();

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("user-sm")]
        public async Task<ActionResult<GameBoardByUserViewModel>> GetGameBoardByUserSimplifiedAsync(CancellationToken cancellationToken)
        {
            var query = new GetGameBoardByUserSimplifiedWithoutIdQuery();

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("last-discarded")]
        public async Task<ActionResult<FrenchCardViewModel>> GetLastDiscardedGameBoardCardAsync(CancellationToken cancellationToken)
        {
            var query = new GetLastDiscardedGameBoardCardQuery();

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("drawable-cards")]
        public async Task<ActionResult<IEnumerable<FrenchCardViewModel>>> GetDrawableGameBoardCardsAsync([FromQuery] int number, CancellationToken cancellationToken)
        {
            var query = new GetGameBoardCardsOnTopQuery(number);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GameBoardViewModel>>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            var query = new GetGameBoardsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet("cards-on-top/{count}")]
        public async Task<ActionResult<IEnumerable<FrenchCardViewModel>>> GetCardsOnTopAsync(int count, CancellationToken cancellationToken)
        {
            var query = new GetGameBoardCardsOnTopQuery(count);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateGameBoardAsync([FromBody] GameBoardDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateGameBoardCommand(dto);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("shuffle-cards")]
        public async Task ShuffleGameBoardCardsAsync(CancellationToken cancellationToken)
        {
            var command = new ShuffleGameBoardCardsCommand();
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("discard-card-from-drawable")]
        public async Task<ActionResult<FrenchCardViewModel>> DiscardGameBoardCardFromDrawableAsync(CancellationToken cancellationToken)
        {
            var command = new DiscardFromDrawableGameBoardCardCommand();

            var result = await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return result;
        }

        [HttpPut("end-turn")]
        public async Task<ActionResult> EndGameBoardTurnAsync(CancellationToken cancellationToken)
        {
            var command = new EndGameBoardTurnCommand();

            await _mediator.Send(command, cancellationToken);
            await GameHub.Refresh(_mediator, _hub, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameboardAsync(long id, CancellationToken cancellationToken)
        {
            var command = new DeleteGameBoardCommand(id);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
