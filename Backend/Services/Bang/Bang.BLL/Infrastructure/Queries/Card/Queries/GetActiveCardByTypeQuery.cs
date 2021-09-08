using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetActiveCardByTypeQuery : IRequest<CardViewModel>
    {
        public ActiveCardType Type { get; set; }

        public GetActiveCardByTypeQuery(ActiveCardType type)
        {
            Type = type;
        }
    }
}
