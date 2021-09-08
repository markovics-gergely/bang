using Bang.BLL.Infrastructure.Queries.Catalog.Card.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Card.Queries
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
