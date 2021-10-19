using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DrawCardCommand : IRequest
    {
        public int Count { get; set; }

        public DrawCardCommand(int count)
        {
            Count = count;
        }
    }
}
