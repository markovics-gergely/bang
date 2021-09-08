using System.Collections.Generic;

using MediatR;
using Bang.BLL.Infrastructure.Queries.Catalog.Character.ViewModels;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Character.Queries
{
    public class GetCharactersQuery : IRequest<IEnumerable<CharacterViewModel>>
    {
    }
}
