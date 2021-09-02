using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class UpdateCharacterCommand : IRequest
    {
        public long Id { get; set; }
        public CharacterDto Dto { get; set; }

        public UpdateCharacterCommand(long id, CharacterDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
