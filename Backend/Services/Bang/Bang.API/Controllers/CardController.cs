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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace Bang.API.Controllers
{
    //[EnableCors(Startup.CorsPolicy)]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CardController
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPut("play-card/{playerCardId}")]
        public async Task<ActionResult> DecrementPlayerHealthAsync(long playerCardId, CancellationToken cancellationToken)
        {
            var command = new PlayCardCommand(playerCardId);

            await _mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }
    }
}
