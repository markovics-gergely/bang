using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
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
