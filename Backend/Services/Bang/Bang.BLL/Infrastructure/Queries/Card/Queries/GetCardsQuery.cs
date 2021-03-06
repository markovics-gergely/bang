using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCardsQuery : IRequest<IEnumerable<CardViewModel>>
    {
    }
}
