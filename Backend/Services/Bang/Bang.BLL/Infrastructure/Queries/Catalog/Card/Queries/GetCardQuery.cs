using Bang.BLL.Infrastructure.Queries.Catalog.Card.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Card.Queries
{
    public class GetCardQuery : IRequest<CardViewModel>
    {
        public int Id { get; set; }

        public GetCardQuery(int id)
        {
            Id = id;
        }
    }
}
