using Bang.BLL.Infrastructure.Queries.Catalog.Character.ViewModels;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Character.Queries
{
    public class GetCharacterQuery : IRequest<CharacterViewModel>
    {
        public int Id { get; set; }

        public GetCharacterQuery(int id)
        {
            Id = id;
        }
    }
}
