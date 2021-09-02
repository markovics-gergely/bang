using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DeleteCharacterCommand : IRequest
    {
        public long Id { get; set; }

        public DeleteCharacterCommand(long id)
        {
            Id = id;
        }
    }
}
