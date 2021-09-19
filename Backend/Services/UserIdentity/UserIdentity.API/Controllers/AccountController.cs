using UserIdentity.BLL.Application.Commands.Commands;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using UserIdentity.BLL.Application.Commands.User.DataTransferObject;

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

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<bool>> CreateAccountAsync(RegistrationDto registerDto, CancellationToken cancellationToken)
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
