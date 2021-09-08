using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetPassiveCardByTypeQuery : IRequest<CardViewModel>
    {
        public PassiveCardType Type { get; set; }

        public GetPassiveCardByTypeQuery(PassiveCardType type)
        {
            Type = type;
        }
    }
}
