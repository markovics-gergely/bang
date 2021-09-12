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

        [HttpGet("{type}")]
        public async Task<ActionResult<CardViewModel>> GetPassiveCardByTypeAsync(CardType type, CancellationToken cancellationToken)
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
    }
}
