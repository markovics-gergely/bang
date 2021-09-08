using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.BLL.Application.Commands.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MediatR;
using Bang.BLL.Infrastructure.Queries.Catalog.Character.Queries;
using Bang.BLL.Infrastructure.Queries.Catalog.Character.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterViewModel>> GetCharacterAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetCharacterQuery(id);

            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<CharacterViewModel>> GetRoleByTypeAsync(CharacterType type, CancellationToken cancellationToken)
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

        [HttpPost]
        public async Task<ActionResult<long>> CreateCharacterAsync([FromBody] CharacterDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateCharacterCommand(dto);

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacterAsync(int id, [FromBody] CharacterDto dto, CancellationToken cancellationToken)
        {
            var command = new UpdateCharacterCommand(id, dto);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCharacterCommand(id);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
