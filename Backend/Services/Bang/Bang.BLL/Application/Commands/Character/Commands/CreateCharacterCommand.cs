using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class CreateCharacterCommand : IRequest<long>
    {
        public CharacterDto Dto { get; set; }

        public CreateCharacterCommand(CharacterDto dto)
        {
            Dto = dto;
        }
    }
}
