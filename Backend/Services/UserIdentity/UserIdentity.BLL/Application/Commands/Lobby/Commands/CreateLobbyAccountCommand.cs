using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateLobbyAccountCommand : IRequest
    {
        public string Password { get; set; }

        public CreateLobbyAccountCommand(string password)
        {
            Password = password;
        }
    }
}
