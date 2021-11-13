using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DrawCardByIdCommand : IRequest
    {
        public long Id { get; set; }

        public DrawCardByIdCommand(long id)
        {
            Id = id;
        }
    }
}
