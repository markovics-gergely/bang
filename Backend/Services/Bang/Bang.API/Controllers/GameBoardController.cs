using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameBoardViewModel>>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            var query = new GetGameBoardsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }
    }
}
