using Bang.BLL.Application.Commands.Player.DataTransferObjects;
using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class UseCharacterCommand : IRequest
    {
        public CharacterDto CharacterDto { get; set; }

        public UseCharacterCommand(CharacterDto characterDto)
        {
            CharacterDto = characterDto;
        }
    }
}
