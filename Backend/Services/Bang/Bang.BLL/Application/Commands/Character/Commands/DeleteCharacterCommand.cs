using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DeleteCharacterCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteCharacterCommand(int id)
        {
            Id = id;
        }
    }
}
