using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class UpdateCharacterCommand : IRequest
    {
        public int Id { get; set; }
        public CharacterDto Dto { get; set; }

        public UpdateCharacterCommand(int id, CharacterDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
