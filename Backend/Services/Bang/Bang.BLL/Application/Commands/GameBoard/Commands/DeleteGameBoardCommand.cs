using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DeleteGameBoardCommand : IRequest
    {
        public long Id { get; set; }

        public DeleteGameBoardCommand(long id)
        {
            Id = id;
        }
    }
}
