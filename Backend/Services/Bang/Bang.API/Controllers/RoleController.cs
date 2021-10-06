using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.BLL.Application.Commands.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.DAL.Domain.Constants.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<RoleViewModel>> GetRoleByTypeAsync(RoleType type, CancellationToken cancellationToken)
        {
            var query = new GetRoleByTypeQuery(type);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var query = new GetRolesQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }
    }
}
