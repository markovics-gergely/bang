using UserIdentity.BLL.Application.Commands.User.DataTransferObject;

using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public RegistrationDto Dto { get; set; }

        public CreateAccountCommand(RegistrationDto dto)
        {
            Dto = dto;
        }
    }
}
