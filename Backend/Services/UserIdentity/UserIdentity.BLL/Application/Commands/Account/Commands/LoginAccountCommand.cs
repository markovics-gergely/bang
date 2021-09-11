using UserIdentity.BLL.Application.Commands.User.DataTransferObject;

using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class LoginAccountCommand : IRequest
    {
        public LoginDto Dto { get; set; }

        public LoginAccountCommand(LoginDto dto)
        {
            Dto = dto;
        }
    }
}
