using Bang.BLL.Infrastructure.Queries.ViewModels;
using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DiscardFromDrawableGameBoardCardCommand : IRequest<FrenchCardViewModel>
    {
    }
}
