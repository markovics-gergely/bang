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


namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameBoardController
    {
        private readonly IMediator _mediator;

        public GameBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBoardViewModel>> GetGameBoardAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetGameBoardQuery(id);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}/last-discarded")]
        public async Task<ActionResult<FrenchCardViewModel>> GetLastDiscardedGameBoardCardAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetLastDiscardedGameBoardCardQuery(id);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}/drawable-cards")]
        public async Task<ActionResult<IEnumerable<FrenchCardViewModel>>> GetLastDiscardedGameBoardCardAsync(int id, [FromQuery] int number, CancellationToken cancellationToken)
        {
            var query = new GetGameBoardCardsOnTopQuery(id, number);

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameBoardViewModel>>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            var query = new GetGameBoardsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateGameBoardAsync([FromBody] GameBoardDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateGameBoardCommand(dto);

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("{id}/shuffle-cards")]
        public async Task ShuffleGameBoardCardsAsync(long id, CancellationToken cancellationToken)
        {
            var command = new ShuffleGameBoardCardsCommand(id);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
