using System.Collections.Generic;

using MediatR;
using Bang.BLL.Infrastructure.Queries.ViewModels;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCharactersQuery : IRequest<IEnumerable<CharacterViewModel>>
    {
    }
}
