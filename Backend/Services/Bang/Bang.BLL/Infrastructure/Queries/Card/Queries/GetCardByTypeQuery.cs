using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCardByTypeQuery : IRequest<CardViewModel>
    {
        public CardType Type { get; set; }

        public GetCardByTypeQuery(CardType type)
        {
            Type = type;
        }
    }
}
