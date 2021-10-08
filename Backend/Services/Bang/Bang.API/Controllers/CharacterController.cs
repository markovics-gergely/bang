﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MediatR;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<CharacterViewModel>> GetCharacterByTypeAsync(CharacterType type, CancellationToken cancellationToken)
        {
            var query = new GetCharacterByTypeQuery(type);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterViewModel>>> GetCharactersAsync(CancellationToken cancellationToken)
        {
            var query = new GetCharactersQuery();

            return (await _mediator.Send(query, cancellationToken)).ToList();
        }
    }
}
