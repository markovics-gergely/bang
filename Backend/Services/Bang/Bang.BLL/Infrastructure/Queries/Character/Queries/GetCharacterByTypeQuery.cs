using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCharacterByTypeQuery : IRequest<CharacterViewModel>
    {
        public CharacterType Type { get; set; }

        public GetCharacterByTypeQuery(CharacterType type)
        {
            Type = type;
        }
    }
}
