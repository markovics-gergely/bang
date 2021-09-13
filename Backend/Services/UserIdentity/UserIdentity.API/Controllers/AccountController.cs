using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UserIdentity.BLL.Application.Commands.User.DataTransferObject;

namespace UserIdentity.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAccountAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var command = new LoginAccountCommand(loginDto);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAccountAsync(string userName, CancellationToken cancellationToken)
        {
            var command = new DeleteAccountCommand(userName);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
