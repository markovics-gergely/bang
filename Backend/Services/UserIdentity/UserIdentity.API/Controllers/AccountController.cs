using UserIdentity.BLL.Application.Commands.Commands;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using UserIdentity.BLL.Application.Commands.User.DataTransferObject;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

namespace UserIdentity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("actual-account")]
        public async Task<ActionResult<string>> GetActualAccountIdAsync(CancellationToken cancellationToken)
        {
            var query = new GetActualAccountIdQuery();

            return await _mediator.Send(query, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("actual-status")]
        public async Task<ActionResult<StatusViewModel>> GetActualAccountStatusAsync(CancellationToken cancellationToken)
        {
            var command = new GetActualAccountStatusQuery();

            return await _mediator.Send(command, cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<bool>> CreateAccountAsync([FromBody]RegistrationDto registerDto, CancellationToken cancellationToken)
        {
            var command = new CreateAccountCommand(registerDto);

            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccountAsync(CancellationToken cancellationToken)
        {
            var command = new DeleteAccountCommand();

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
