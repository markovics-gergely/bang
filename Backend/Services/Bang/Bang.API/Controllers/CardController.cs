using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Bang.DAL.Domain.Constants.Enums;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardViewModel>> GetCardAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetCardQuery(id);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("active/{type}")]
        public async Task<ActionResult<CardViewModel>> GetActiveCardByTypeAsync(ActiveCardType type, CancellationToken cancellationToken)
        {
            var query = new GetActiveCardByTypeQuery(type);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("passive/{type}")]
        public async Task<ActionResult<CardViewModel>> GetPassiveCardByTypeAsync(PassiveCardType type, CancellationToken cancellationToken)
        {
            var query = new GetPassiveCardByTypeQuery(type);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardViewModel>>> GetCardsAsync(CancellationToken cancellationToken)
        {
            var query = new GetCardsQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }
    }
}
