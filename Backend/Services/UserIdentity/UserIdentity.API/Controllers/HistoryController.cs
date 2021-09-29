using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain.Bang;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using UserIdentity.DAL.Domain.Bang;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryViewModel>>> GetHistoriesAsync(CancellationToken cancellationToken)
        {
            var query = new GetHistoriesQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistoryAsync(RoleType playedRole, int placement, CancellationToken cancellationToken)
        {
            var command = new CreateHistoryCommand(playedRole, placement);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

    }
}
